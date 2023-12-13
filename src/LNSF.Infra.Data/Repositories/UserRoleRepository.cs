using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context;

    public UserRoleRepository(UserManager<IdentityUser> userManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<List<IdentityUserRole<string>>> Query(UserRoleFilter filter)
    {
        var query = _context.UserRoles.AsNoTracking();

        if (!filter.UserId.IsNullOrEmpty()) query = query.Where(ur => ur.UserId == filter.UserId);
        if (!filter.RoleId.IsNullOrEmpty()) query = query.Where(ur => ur.RoleId == filter.RoleId);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(ur => ur.RoleId);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(ur => ur.RoleId);

        var usersroles = await query
            .Skip((filter.Page?.Page -1 ) * filter.Page?.PageSize ?? 0)
            .Take(filter.Page?.PageSize ?? 0)
            .ToListAsync();

        return usersroles;
    }

    public async Task<int> GetCount() => 
        await _context.UserRoles.AsNoTracking().CountAsync();

    public async Task<bool> ExistsByUserAndRoleName(IdentityUser user, string roleName) => 
        await _userManager.IsInRoleAsync(user, roleName);
    
    public async Task<bool> ExistsByUserIdAndRoleName(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<bool> Add(IdentityUser user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded ? true : throw new AppException("Erro ao adicionar usuário ao perfil!", HttpStatusCode.BadRequest);
    }

    public async Task<bool> Remove(IdentityUser user, string roleName)
    {
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return result.Succeeded ? true : throw new AppException("Erro ao remover usuário do perfil!", HttpStatusCode.BadRequest);
    }
}
