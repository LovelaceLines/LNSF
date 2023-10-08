using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IEmergencyContactService
{
    public Task<List<EmergencyContact>> Query(EmergencyContactFilter filter);
    public Task<int> GetCount();
    public Task<EmergencyContact> Create(EmergencyContact contact);
    public Task<EmergencyContact> Update(EmergencyContact contact);
    public Task<EmergencyContact> Delete(int id);
}
