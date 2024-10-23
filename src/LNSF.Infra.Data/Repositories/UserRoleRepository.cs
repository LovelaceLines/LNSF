using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class UserRoleRepository(AppDbContext context) : BaseRepository<UserRole>(context), IUserRoleRepository
{
	public async Task<bool> Exists(int userId, int roleId) =>
		await context.UserRoles.AsNoTracking().AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

	public async Task<UserRole> Get(int userId, int roleId) =>
		await context.UserRoles.AsNoTracking().FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId) ??
			throw new AppException("Relação não encontrada!", HttpStatusCode.NotFound);

	public async Task<UserRole> Add(int userId, int roleId)
	{
		UserRole userRole = new()
		{
			UserId = userId,
			RoleId = roleId,
		};

		return await Add(userRole);
	}
}
