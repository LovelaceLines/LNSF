using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
	Task<QueryResult<Role>> Query(RoleFilter filter);
	Task<bool> ExistsById(int id);
	Task<bool> ExistsByName(string name);
	Task<Role> GetById(int id);
	Task<Role> GetByName(string name);
	Task<List<Role>> GetByUser(int userId);
	// new Task<Role> Add(Role role);
	new Task<Role> Update(Role role);
	new Task<Role> Remove(Role role);
}
