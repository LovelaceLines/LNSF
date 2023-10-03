using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    public Task<bool> Exist(AccountFilters filters);
}
