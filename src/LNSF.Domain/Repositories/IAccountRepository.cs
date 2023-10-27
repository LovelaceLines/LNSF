using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    public Task<List<Account>> Query(AccountFilter filter);
    public Task<Account> GetById(string id);
    public Task<Account> GetByUserName(string userName);
    public Task<Account> Auth(string userName, string password);
    public Task<bool> ExistsId(string id);
    public Task<bool> Exists(string userName, string password);
    public Task<bool> Exists(string userName);

    public string HashPassword(string password);
    public bool VerifyPassword(string password, string hashPassword);

}
