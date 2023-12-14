using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LNSF.Application.Services;

public class AuthenticationTokenService : IAuthenticationTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthenticationTokenService(IConfiguration configuration,
        IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationToken> Login(string userName, string password)
    {   
        var user = await _userRepository.Auth(userName, password);
        return await GetAuthenticationToken(user);
    }

    public async Task<AuthenticationToken> RefreshToken(string token)
    {
        var result = await new JsonWebTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = SecurityKey,
        });
        if (!result.IsValid) throw new AppException("Acess Token expired!", HttpStatusCode.Unauthorized);
        
        var userId = result.Claims["nameid"].ToString() ?? throw new AppException("Claims NameId not found!", HttpStatusCode.InternalServerError);
        var user = await _userRepository.GetById(userId);
        return await GetAuthenticationToken(user);
    }

    private async Task<AuthenticationToken> GetAuthenticationToken(IdentityUser user) =>
        new(token: await GenerateAccessToken(user), Expires);

    private async Task<string> GenerateAccessToken(IdentityUser user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = Issuer,
            Audience = Audience,
            Subject = await Subject(user),
            Expires = Expires,
            SigningCredentials = SigningCredentials,
            TokenType = "at+jwt"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials SigningCredentials => 
        new(SecurityKey, SecurityAlgorithms.HmacSha256);

    private SecurityKey SecurityKey =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["SecretKey"] ?? throw new AppException("JwtConfig: Secret is null!", HttpStatusCode.InternalServerError)));

    private string Issuer =>
        _configuration["Issuer"] ?? throw new AppException("JwtConfig: Issuer is null!", HttpStatusCode.InternalServerError);
    
    private string Audience =>
        _configuration["Audience"] ?? throw new AppException("JwtConfig: Audience is null!", HttpStatusCode.InternalServerError);

    private DateTime Expires =>
        DateTime.UtcNow.AddHours(int.Parse(
            _configuration["ExpireHours"] ?? throw new AppException("JwtConfig: ExpireHours is null!", HttpStatusCode.InternalServerError)));

    private async Task<ClaimsIdentity> Subject(IdentityUser user) =>
        new(await GetClaims(user));

    private async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!)
        };

        var roles = await _userRepository.GetRoles(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}
