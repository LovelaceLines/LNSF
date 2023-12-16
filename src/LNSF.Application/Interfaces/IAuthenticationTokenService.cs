using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IAuthenticationTokenService
{
    Task<AuthenticationToken> Login(string userName, string password);
    Task<AuthenticationToken> RefreshToken(string token);
    Task<UserDTO> GetUser(string token);
}
