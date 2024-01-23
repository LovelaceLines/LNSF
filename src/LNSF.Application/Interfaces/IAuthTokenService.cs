using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IAuthTokenService
{
    Task<AuthToken> Login(string userName, string password);
    Task<AuthToken> RefreshToken(string refreshToken);
    Task<UserDTO> GetUser(string acessToken);
}
