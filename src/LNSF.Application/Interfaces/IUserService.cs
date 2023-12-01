using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Interfaces;

public interface IUserService
{
    Task<List<IdentityUser>> Query(UserFilter filter);
    Task<IdentityUser> Create(IdentityUser user);
    Task<IdentityUser> Update(IdentityUser user);
    Task<IdentityUser> UpdatePassword(string id, string oldPassword, string newPassword);
    Task<IdentityUser> Delete(string id);
}
