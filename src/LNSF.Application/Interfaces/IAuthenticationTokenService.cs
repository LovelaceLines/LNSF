using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IAuthenticationTokenService
{
    Task<AuthenticationToken> Login(Account account);
    Task<AuthenticationToken> RefreshToken(AuthenticationToken token);
}
