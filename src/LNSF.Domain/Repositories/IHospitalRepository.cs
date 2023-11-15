using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHospitalRepository : IBaseRepository<Hospital>
{
    Task<List<Hospital>> Query(HospitalFilter filter);
    Task<bool> ExistsByName(string name);
}
