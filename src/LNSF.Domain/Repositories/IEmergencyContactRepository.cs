using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactRepository : IBaseRepository<EmergencyContact>
{
	Task<QueryResult<EmergencyContact>> Query(EmergencyContactFilter filter);
	Task<List<EmergencyContact>> GetByPeopleId(int peopleId);
	Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
}
