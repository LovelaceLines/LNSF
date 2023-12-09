using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface ITreatmentRepository : IBaseRepository<Treatment>
{
    Task<List<Treatment>> Query(TreatmentFilter filter);
    Task<bool> ExistsByName(string name);
    Task<bool> ExistsByNameAndType(string name, TypeTreatment type);
}

