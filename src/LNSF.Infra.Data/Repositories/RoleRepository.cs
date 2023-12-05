using System.Net;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(RoleManager<IdentityRole> roleManager) => 
        _roleManager = roleManager;
    
    public async Task<List<IdentityRole>> Query(RoleFilter filter)
    {
        var query = _roleManager.Roles.AsNoTracking();

        if (!filter.Id.IsNullOrEmpty()) query = query.Where(x => x.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(x => x.Name!.Contains(filter.Name!));

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Name);
        else query = query.OrderByDescending(x => x.Name);

        var roles = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return roles;
    }

    public async Task<int> GetCount() =>
        await _roleManager.Roles.AsNoTracking().CountAsync();

    public async Task<bool> ExistsById(string id) =>
        await _roleManager.FindByIdAsync(id) != null;

    public Task<bool> ExistsByName(string name) => 
        _roleManager.RoleExistsAsync(name);

    public async Task<IdentityRole> GetById(string id) => 
        await _roleManager.FindByIdAsync(id) ?? throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

    public async Task<IdentityRole> GetByName(string name) => 
        await _roleManager.FindByNameAsync(name) ?? throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

    public async Task<IdentityRole> Add(IdentityRole role)
    {
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return role;
    }

    public async Task<IdentityRole> Update(IdentityRole role)
    {
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return role;
    }

    public async Task<IdentityRole> Remove(string name)
    {
        var role = await GetByName(name);
        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return role;
    }
}
