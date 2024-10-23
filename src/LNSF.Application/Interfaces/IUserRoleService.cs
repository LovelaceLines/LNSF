using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IUserRoleService
{
	Task<User> AddToRole(int userId, int roleId);
	Task<bool> RemoveFromRole(int userId, int roleId);
}
