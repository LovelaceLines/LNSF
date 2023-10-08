using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IAccountService
{
    public Task<List<Account>> Query(AccountFilter filter);
    public Task<Account> Get(string userName, string password);
    public Task<Account> Create(Account account);
    public Task<Account> Update(Account account);
    public Task<Account> Delete(string id);
}
