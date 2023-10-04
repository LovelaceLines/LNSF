using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactsRepository : IBaseRepository<EmergencyContact>
{
    public Task<List<EmergencyContact>> Query(EmergencyContactFilter filter);
}
