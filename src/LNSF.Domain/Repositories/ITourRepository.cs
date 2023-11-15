using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface ITourRepository : IBaseRepository<Tour>
{
    public Task<List<Tour>> Query(TourFilter filter);
    public Task<bool> IsOpen(int id);
    public Task<bool> IsClosed(int id);
    public Task<bool> PeopleHasOpenTour(int peopleId);
}
