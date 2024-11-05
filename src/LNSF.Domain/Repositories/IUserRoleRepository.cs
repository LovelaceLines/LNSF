using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
	Task<bool> ExistsByUserIdRoleId(int userId, int roleId);
	Task<UserRole> GetByUserIdRoleId(int userId, int roleId);
}
