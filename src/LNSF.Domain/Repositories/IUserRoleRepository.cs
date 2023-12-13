using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.Repositories;

public interface IUserRoleRepository
{
    Task<List<IdentityUserRole<string>>> Query(UserRoleFilter filter);
    Task<int> GetCount();
    Task<bool> ExistsByUserAndRoleName(IdentityUser user, string roleName);
    Task<bool> ExistsByUserIdAndRoleName(string userId, string roleName);
    Task<bool> Add(IdentityUser user, string roleName);
    Task<bool> Remove(IdentityUser user, string roleName);
}
