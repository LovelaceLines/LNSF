using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IUserRoleService
{
	Task<UserRole> AddToRole(int userId, int roleId);
	Task<UserRole> RemoveFromRole(int userId, int roleId);
}
