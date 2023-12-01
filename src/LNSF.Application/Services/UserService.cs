using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(UserManager<IdentityUser> userManager) => 
        _userManager = userManager;
    
    public Task<List<IdentityUser>> Query(UserFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser> Create(IdentityUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser> Update(IdentityUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser> UpdatePassword(string id, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser> Delete(string id)
    {
        throw new NotImplementedException();
    }
}
