using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactRepository : IBaseRepository<EmergencyContact>
{
    public Task<List<EmergencyContact>> Query(EmergencyContactFilter filter);
}
