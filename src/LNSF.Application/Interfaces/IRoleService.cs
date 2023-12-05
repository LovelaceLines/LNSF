using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Interfaces;

public interface IRoleService
{
    Task<List<IdentityRole>> Query(RoleFilter filter);
    Task<int> GetCount();
    Task<IdentityRole> Add(IdentityRole role);
    Task<IdentityRole> Update(IdentityRole role);
    Task<IdentityRole> Delete(string name);
}
