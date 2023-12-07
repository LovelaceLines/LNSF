using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Application.Services;

public class AuthenticationTokenService : IAuthenticationTokenService
{
    private readonly IConfiguration _configuration;
    private readonly AuthenticationTokenValidator _validator;
    private readonly IUserRepository _userRepository;

    public AuthenticationTokenService(IConfiguration configuration,
        AuthenticationTokenValidator validator,
        IUserRepository userRepository)
    {
        _configuration = configuration;
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationToken> Login(string userName, string password)
    {   
        var user = await _userRepository.Auth(userName, password);
        var token = new AuthenticationToken
        {
            AccessToken = await GenerateAccessToken(user),
            RefreshToken = GenerateRefreshToken(user),
            Expires = DateTime.Now.AddHours(ExpireHoursAccessToken),
        };
        return token;
    }
    
    public async Task<AuthenticationToken> RefreshToken(AuthenticationToken token)
    {
        var validationResult = _validator.Validate(token);
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
        
        var userId = result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.NotFound);
        var user = await _userRepository.GetById(userId);

        var newToken = new AuthenticationToken
        {
            AccessToken = await GenerateAccessToken(user),
            RefreshToken = GenerateRefreshToken(user),
            Expires = DateTime.Now.AddHours(ExpireHoursAccessToken),
        };
        return newToken;
    }

    private async Task<string> GenerateAccessToken(IdentityUser user)
    {
        var claims = await GetClaims(user);
        var expireHours = ExpireHoursAccessToken;
        var key = GetSecretKey();

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

    private string GenerateRefreshToken(IdentityUser user)
    {
        var claims = GetClaims(user.Id.ToString());
        var key = GetSecretKey();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Issuer,
            Audience = Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(ExpireHoursRefreshToken),
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

    private Claim[] GetClaims(string userId) => 
        new[] { new Claim(ClaimTypes.NameIdentifier, userId) };

    private async Task<Claim[]> GetClaims(IdentityUser user)
    {
        var role = await _userRepository.GetRoles(user);
        return new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Name, user.UserName!)
        };
    }
}
