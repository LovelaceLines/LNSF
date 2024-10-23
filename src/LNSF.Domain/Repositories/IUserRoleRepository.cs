using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
	Task<bool> Exists(int userId, int roleId);
	Task<UserRole> Get(int userId, int roleId);
	Task<UserRole> Add(int userId, int roleId);
}
