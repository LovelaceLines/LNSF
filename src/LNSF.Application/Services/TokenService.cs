using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace LNSF.Application.Services;

public class TokenService
{
    public TokenService(ConfigurationManager configuration)
    {
        
    }
    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("MySecretKey");
    }
}
