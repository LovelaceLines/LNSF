using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IFamilyGroupProfileRepository : IBaseRepository<FamilyGroupProfile>
{
    Task<List<FamilyGroupProfile>> Query(FamilyGroupProfileFilter filter);
}
