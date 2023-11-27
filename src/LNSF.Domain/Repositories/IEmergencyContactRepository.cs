using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactRepository : IBaseRepository<EmergencyContact>
{
    Task<List<EmergencyContact>> Query(EmergencyContactFilter filter);
    Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
}
