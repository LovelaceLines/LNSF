using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IEscortRepository : IBaseRepository<Escort>
{
	Task<QueryResult<Escort>> Query(BaseFilter filter);
	Task<bool> ExistsByPeopleId(int peopleId);
	Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
	Task<List<Escort>> GetByHostingId(int hostingId);
}
