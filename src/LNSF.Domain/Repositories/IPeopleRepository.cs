using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPeopleRepository : IBaseRepository<People>
{
    Task<List<PeopleDTO>> Query(PeopleFilter filter);
}
