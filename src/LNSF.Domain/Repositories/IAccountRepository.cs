using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    public Task<List<Account>> Query(AccountFilter filter);
    public Task<Account> Get(string userName, string Password);
    public Task<bool> Exists(string userName, string password);
}
