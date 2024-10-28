using AutoFilterer.Extensions;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Infra.Data.Context;
using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class RoleRepository(AppDbContext context, RoleManager<Role> roleManager) : BaseRepository<Role>(context), IRoleRepository
{
	public async Task<QueryResult<Role>> Query(RoleFilter filter)
	{
		var query = context.Roles.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Role>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsById(int id) =>
		await context.Roles.AsNoTracking().AnyAsync(r => r.Id == id);

	public async Task<bool> ExistsByName(string name) =>
		await context.Roles.AsNoTracking().AnyAsync(r => r.Name == name);

	public async Task<Role> GetById(int id) =>
		await context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id) ??
			throw new AppException("Permissão não encontrada!", HttpStatusCode.NotFound);

	public async Task<Role> GetByName(string name) =>
		await context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name) ??
			throw new AppException("Permissão não encontrada!", HttpStatusCode.NotFound);

	public async Task<List<Role>> GetByUser(int userId) =>
		await context.Roles.AsNoTracking()
			.Join(context.UserRoles.AsNoTracking(), r => r.Id, ur => ur.RoleId, (r, ur) => new { r, ur })
			.Where(rur => rur.ur.UserId == userId)
			.Select(rur => rur.r)
			.ToListAsync();

	// public async new Task<Role> Add(Role role)
	// {
	//     var result = await roleManager.CreateAsync(role);

	//     return result.Succeeded ? role :
	//         throw new AppException("Não foi possível criar a permissão!", HttpStatusCode.BadRequest);
	// }

	public async new Task<Role> Update(Role role)
	{
		var result = await roleManager.UpdateAsync(role);

		return result.Succeeded ? role :
			throw new AppException("Não foi possível atualizar a permissão!", HttpStatusCode.BadRequest);
	}

	public async new Task<Role> Remove(Role role)
	{
		var result = await roleManager.DeleteAsync(role);

		return result.Succeeded ? role :
			throw new AppException("Não foi possível remover a permissão!", HttpStatusCode.BadRequest);
	}
}
