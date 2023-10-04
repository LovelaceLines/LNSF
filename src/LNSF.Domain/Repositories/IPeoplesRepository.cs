using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IPeoplesRepository : IBaseRepository<People>
{
    public Task<List<People>> Query(PeopleFilter filter);
}
