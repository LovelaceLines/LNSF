using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IAuthenticationTokenRepository : IBaseRepository<AuthenticationToken>
{
    public Task<AuthenticationToken> Get(string token, string refreshToken);
    public Task<AuthenticationToken> Get(AuthenticationToken token);
    public Task<bool> Exists(AuthenticationToken token);
}
