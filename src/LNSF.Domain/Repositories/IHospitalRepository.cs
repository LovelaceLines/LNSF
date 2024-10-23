using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHospitalRepository : IBaseRepository<Hospital>
{
	Task<QueryResult<Hospital>> Query(HospitalFilter filter);
	Task<bool> ExistsByName(string name);
}
