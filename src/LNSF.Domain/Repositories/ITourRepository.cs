using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface ITourRepository : IBaseRepository<Tour>
{
    Task<List<Tour>> Query(TourFilter filter);
    Task<bool> IsOpen(int id);
    Task<bool> IsClosed(int id);
    Task<bool> PeopleHasOpenTour(int peopleId);
    Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
}
