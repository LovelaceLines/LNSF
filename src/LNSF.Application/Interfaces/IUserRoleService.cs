using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IUserRoleService
{
	Task<UserRole> AddToRole(UserRole userRole);
	Task<UserRole> RemoveFromRole(UserRole userRole);
}
