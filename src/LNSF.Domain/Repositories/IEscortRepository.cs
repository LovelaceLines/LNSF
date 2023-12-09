using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IEscortRepository : IBaseRepository<Escort>
{
    Task<List<Escort>> Query(EscortFilter filter);
    Task<bool> ExistsByPeopleId(int peopleId);  
    Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
}
