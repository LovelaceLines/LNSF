using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IAccountService
{
    public Task<List<Account>> Query(AccountFilter filter);
    public Task<int> GetCount();
    public Task<Account> Get(string userName);
    public Task<Account> Create(Account account);
    public Task<Account> Update(Account account);
    public Task<Account> Update(string id, string newPasswor, string oldPassword);
    public Task<Account> Delete(string id);
}
