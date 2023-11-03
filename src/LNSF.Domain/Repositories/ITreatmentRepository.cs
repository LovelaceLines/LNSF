using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface ITreatmentRepository : IBaseRepository<Treatment>
{
    public Task<List<Treatment>> Query(TreatmentFilter filter);
    public Task<bool> NameExists(string name);
}

