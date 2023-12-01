using System.Net;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class UserRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IdentityUserRole<string> _userRoleManager;

    public UserRepository(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IdentityUserRole<string> userRoleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRoleManager = userRoleManager;
    }

    public async Task<List<IdentityUser>> Query(UserFilter filter)
    {
        var query = _userManager.Users.AsNoTracking();

        if (!filter.Id.IsNullOrEmpty()) query = query.Where(u => u.Id == filter.Id);
        if (!filter.UserName.IsNullOrEmpty()) query = query.Where(u => u.UserName != null && u.UserName.ToLower().Contains(filter.UserName!.ToLower()));
        if (!filter.Email.IsNullOrEmpty()) query = query.Where(u => u.Email != null && u.Email.ToLower().Contains(filter.Email!.ToLower()));
        if (filter.Role.HasValue) query = query.Where(u =>
            _roleManager.Roles.Any(r => r.Name == filter.Role.ToString() && 
                _userRoleManager.RoleId == r.Id && _userRoleManager.UserId == u.Id));
        
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(u => u.UserName);
        else query = query.OrderByDescending(u => u.UserName);

        var users = await query
            .Skip((filter.Page?.Page -1 ) * filter.Page?.PageSize ?? 0)
            .Take(filter.Page?.PageSize ?? 0)
            .ToListAsync();

        return users;
    }

    public async Task<IdentityUser> Add(IdentityUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) throw new AppException(result.Errors.ToString()!, HttpStatusCode.BadRequest);

        return user;
    }
}
