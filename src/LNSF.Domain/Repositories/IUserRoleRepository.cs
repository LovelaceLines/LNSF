using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.Repositories;

public interface IUserRoleRepository
{
    Task<List<IdentityUserRole<string>>> Query(UserRoleFilter filter);
    Task<int> GetCount();
    Task<bool> ExistsByUserAndRoleName(IdentityUser user, string role);
    Task<bool> Add(IdentityUser user, string role);
    Task<bool> Remove(IdentityUser user, string role);
}
