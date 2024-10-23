using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IRoleService
{
	Task<Role> GetById(int id);
	Task<Role> GetByName(string name);
	Task<List<Role>> GetByUser(int id);
	Task<Role> Create(Role role);
	Task<Role> Update(Role role);
	Task<Role> Delete(int id);
}
