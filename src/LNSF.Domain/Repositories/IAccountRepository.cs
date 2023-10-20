using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    public Task<List<Account>> Query(AccountFilter filter);
    public Task<Account> Get(string id);
    public Task<Account> Get(string userName, string Password);
    public Task<bool> ExistsId(string id);
    public Task<bool> Exists(string userName, string password, string hashPassword);
    public Task<bool> Exists(string userName);

    public string HashPassword(string password);
    public bool VerifyPassword(string password, string hashPassword);

}
