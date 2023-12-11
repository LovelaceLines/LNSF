using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Interfaces;

public interface IUserRoleService
{
    Task<List<IdentityUserRole<string>>> Query(UserRoleFilter filter);
    Task<int> GetCount();
}
