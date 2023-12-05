namespace LNSF.Domain.Repositories;

using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

public interface IRoleRepository
{
    Task<List<IdentityRole>> Query(RoleFilter filter);
    Task<int> GetCount();
    Task<bool> ExistsById(string id);
    Task<bool> ExistsByName(string name);
    Task<IdentityRole> GetById(string id);
    Task<IdentityRole> GetByName(string name);
    Task<IdentityRole> Add(IdentityRole role);
    Task<IdentityRole> Update(IdentityRole role);
    Task<IdentityRole> Remove(string name);
}
