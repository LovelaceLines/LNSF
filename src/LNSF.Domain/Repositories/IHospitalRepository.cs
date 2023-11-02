using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHospitalRepository : IBaseRepository<Hospital>
{
    public Task<List<Hospital>> Query(HospitalFilter filter);
    public Task<bool> Exists(string name);
}
