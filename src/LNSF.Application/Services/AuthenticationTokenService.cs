using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Application.Services;

public class AuthenticationTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticationTokenRepository _authTokenRepository;
    private readonly IAccountRepository _accountRepository;

    public AuthenticationTokenService(IConfiguration configuration,
        IAuthenticationTokenRepository authTokenRepository,
        IAccountRepository accountRepository)
    {
        _configuration = configuration;
        _authTokenRepository = authTokenRepository;
        _accountRepository = accountRepository;
    }

    public async Task<AuthenticationToken> Login(Account account)
    {
        if (!await _accountRepository.Exists(account.UserName, account.Password))
            throw new AppException("Usuário ou senha inválidos", HttpStatusCode.Unauthorized);
        
        account = await _accountRepository.Get(account.UserName, account.Password);
        var token = new AuthenticationToken
        {
            Token = GenerateToken(account),
            RefreshToken = GenerateRefreshToken(),
            AccountId = account.Id,
        };
        return await _authTokenRepository.Add(token);
    }
    
    public async Task<AuthenticationToken> RefreshToken(AuthenticationToken token)
    {
        if (!await _authTokenRepository.Exists(token))
            throw new AppException("Token inválido", HttpStatusCode.Unauthorized);
        if (!IsExpired(token.Token)) 
            throw new AppException("Not expired token", HttpStatusCode.NotModified);

        var account = await _accountRepository.Get(token.AccountId);
        await _authTokenRepository.Remove(token);
        token = new AuthenticationToken()
        {
            Token = GenerateToken(account),
            RefreshToken = GenerateRefreshToken(),
            AccountId = token.AccountId
        };
        return await _authTokenRepository.Add(token);
    }

    public async Task<bool> Logout(AuthenticationToken token)
    {
        await _authTokenRepository.Remove(token);
        return true;
    }

    private string GenerateToken(Account account)
    {
        var claims = GetClaims(account);
        var key = GetSecretKey();
        var expireHours = GetExpireHours();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expireHours),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private int GetExpireHours()
    {
        return int.Parse(_configuration["ExpireHours"] ?? "8");
    }

    private byte[] GetSecretKey()
    {
        var secretKey = _configuration["SecretKey"] ?? throw new InvalidOperationException("JwtConfig: Secret is null");
        var key = Encoding.ASCII.GetBytes(secretKey);

        return key;
    }

    private static Claim[] GetClaims(Account account)
    {
        return new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Role, account.Role.ToString()),
            new Claim(ClaimTypes.Name, account.UserName)
        };
    }

    private bool IsExpired(string token)
    {
        var key = GetSecretKey();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
                token, tokenValidationParameters, out var securityToken);
                
        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
            StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return jwtSecurityToken.ValidTo < DateTime.UtcNow;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = GetSecretKey();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token, tokenValidationParameters, out var securityToken);
        
        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
            StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public async Task<List<AuthenticationToken>> Get() => 
        await _authTokenRepository.Get();
}
