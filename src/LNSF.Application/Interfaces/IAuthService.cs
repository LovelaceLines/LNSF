using LNSF.Domain.DTOs;

namespace LNSF.Application.Interfaces;

public interface IAuthService
{
	Task<AuthToken> Login(string userName, string password);
	Task<AuthToken> RefreshToken(string refreshToken);
	Task<UserDTO> GetUser(string acessToken);
	Task<(int, string[])> GetUserIds(string acessToken);
}
