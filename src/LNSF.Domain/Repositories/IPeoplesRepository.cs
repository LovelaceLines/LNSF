using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IPeoplesRepository : IBaseRepository<People>
{
    public Task<List<People>> Get(PeopleFilters filters);
}
