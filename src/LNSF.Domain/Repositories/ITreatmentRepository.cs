using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface ITreatmentRepository : IBaseRepository<Treatment>
{
    public Task<List<Treatment>> Query(TreatmentFilter filter);
    public Task<bool> ExistsByName(string name);
    public Task<bool> ExistsByNameAndType(string name, TypeTreatment type);
}

