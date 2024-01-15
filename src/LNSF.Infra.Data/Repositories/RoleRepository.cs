using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IQueryable<IdentityRole> _roles;

    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _roles = _roleManager.Roles.AsNoTracking();
    }

    public async Task<List<IdentityRole>> Query(RoleFilter filter)
    {
        var query = _roles;

        if (!filter.Id.IsNullOrEmpty()) query = query.Where(r => r.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(r => r.Name!.Contains(filter.Name!));

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(r => r.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(r => r.Name);

        var roles = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return roles;
    }

    public async Task<int> GetCount() =>
        await _roles.CountAsync();

    public async Task<bool> ExistsById(string id) =>
        await _roles.AnyAsync(r => r.Id == id);

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
