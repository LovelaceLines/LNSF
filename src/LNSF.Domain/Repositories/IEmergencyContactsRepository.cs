using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactsRepository : IBaseRepository<EmergencyContact>
{
    public Task<List<EmergencyContact>> Get(EmergencyContactFilters filters);
    public Task<EmergencyContact> Get(int peopleId, string phone);
}
