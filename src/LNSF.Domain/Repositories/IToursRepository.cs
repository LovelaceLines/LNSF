using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IToursRepository : IBaseRepository<Tour>
{
    public Task<List<Tour>> Query(TourFilters filters);
}
