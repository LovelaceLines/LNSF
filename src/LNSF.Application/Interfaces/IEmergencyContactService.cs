using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IEmergencyContactService
{
    Task<List<EmergencyContact>> Query(EmergencyContactFilter filter);
    Task<int> GetCount();
    Task<EmergencyContact> Create(EmergencyContact contact);
    Task<EmergencyContact> Update(EmergencyContact contact);
    Task<EmergencyContact> Delete(int id);
}
