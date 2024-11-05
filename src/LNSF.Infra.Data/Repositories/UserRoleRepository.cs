using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class UserRoleRepository(AppDbContext context) : BaseRepository<UserRole>(context), IUserRoleRepository
{
	public async Task<bool> ExistsByUserIdRoleId(int userId, int roleId) =>
		await context.UserRoles.AsNoTracking().AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

	public async Task<UserRole> GetByUserIdRoleId(int userId, int roleId) =>
		await context.UserRoles.AsNoTracking().FirstAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
}
