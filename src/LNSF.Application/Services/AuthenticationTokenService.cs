using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Application.Services;

public class AuthenticationTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IAccountRepository _accountRepository;
    private readonly AuthenticationTokenValidator _tokenValidator;

    public AuthenticationTokenService(IConfiguration configuration,
        IAccountRepository accountRepository,
        AuthenticationTokenValidator tokenValidator)
    {
        _configuration = configuration;
        _accountRepository = accountRepository;
        _tokenValidator = tokenValidator;
    }

    public async Task<AuthenticationToken> Login(Account account)
    {
        if (!await _accountRepository.Exists(account.UserName, account.Password))
            throw new AppException("Usuário ou senha inválidos!", HttpStatusCode.Unauthorized);
        
        account = await _accountRepository.Get(account.UserName, account.Password);
        var token = new AuthenticationToken
        {
            AccessToken = GenerateAccessToken(account),
            RefreshToken = GenerateRefreshToken(account),
            Expires = DateTime.Now.AddHours(ExpireHoursAccessToken),
        };
        return token;
    }
    
    public async Task<AuthenticationToken> RefreshToken(AuthenticationToken token)
    {
        var validationResult = _tokenValidator.Validate(token);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.UnprocessableEntity);

        var key = GetSecretKey();
        var tokenHandler = new JsonWebTokenHandler();
        var result = tokenHandler.ValidateToken(token.RefreshToken, new TokenValidationParameters()
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        });
        if (!result.IsValid) throw new AppException("Token de atualização expirado!", HttpStatusCode.Unauthorized);
        
        var expires = DateTime.Parse(result.Claims["exp"].ToString() ?? throw new AppException("Token de atualização inválido!", HttpStatusCode.InternalServerError));
        var accountId = result.Claims["nameid"].ToString(); // accountId = NameIdentifier
        var account = await _accountRepository.Get(accountId ?? throw new AppException("Token de atualização inválido!", HttpStatusCode.InternalServerError));

        var newToken = new AuthenticationToken
        {
            AccessToken = GenerateAccessToken(account),
            RefreshToken = GenerateRefreshToken(account),
            Expires = DateTime.Now.AddHours(ExpireHoursAccessToken),
        };
        return newToken;
    }

    private string GenerateAccessToken(Account account)
    {
        var claims = GetClaims(account);
        var key = GetSecretKey();
        var expireHours = ExpireHoursAccessToken;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Issuer,
            Audience = Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expireHours),
            SigningCredentials = GetSigningCredentials(key),
            TokenType = "at+jwt"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken(Account account)
    {
        var claims = GetClaims(account.Id.ToString());
        var key = GetSecretKey();
        var expireHours = ExpireHoursRefreshToken;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Issuer,
            Audience = Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expireHours),
            SigningCredentials = GetSigningCredentials(key),
            TokenType = "rt+jwt"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static SigningCredentials GetSigningCredentials(byte[] key) => 
        new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

    private string Issuer =>
        _configuration["Issuer"] ?? throw new AppException("JwtConfig: Issuer é nulo!", HttpStatusCode.NotImplemented);
    
    private string Audience =>
        _configuration["Audience"] ?? throw new AppException("JwtConfig: Audience é nulo!", HttpStatusCode.NotImplemented);

    private int ExpireHoursAccessToken =>
        int.Parse(_configuration["ExpireHoursAccessToken"] ?? throw new AppException("JwtConfig: ExpireHoursAccessToken é nulo!", HttpStatusCode.NotImplemented));

    private int ExpireHoursRefreshToken =>
        int.Parse(_configuration["ExpireHoursRefreshToken"] ?? throw new AppException("JwtConfig: ExpireHoursRefreshToken é nulo!", HttpStatusCode.NotImplemented));

    private byte[] GetSecretKey()
    {
        var secretKey = _configuration["SecretKey"] ?? throw new AppException("JwtConfig: Secret é nulo!", HttpStatusCode.NotImplemented);
        var key = Encoding.ASCII.GetBytes(secretKey);

        return key;
    }

    private static Claim[] GetClaims(string accountId) => 
        new[] { new Claim(ClaimTypes.NameIdentifier, accountId) };

    private static Claim[] GetClaims(Account account) => 
        new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Role, account.Role.ToString()),
            new Claim(ClaimTypes.Name, account.UserName)
        };
}
