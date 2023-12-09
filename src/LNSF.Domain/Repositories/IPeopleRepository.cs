using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPeopleRepository : IBaseRepository<People>
{
    Task<List<People>> Query(PeopleFilter filter);
}
