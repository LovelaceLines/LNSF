using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    public Task<bool> Exist(Account account);
}
