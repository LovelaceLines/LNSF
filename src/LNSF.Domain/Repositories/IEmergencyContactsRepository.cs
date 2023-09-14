using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactsRepository
{
    public Task<ResultDTO<List<EmergencyContact>>> Get(EmergencyContactFilters filters);
    public Task<ResultDTO<EmergencyContact>> Get(int id);
    public Task<ResultDTO<EmergencyContact>> Get(int peopleId, string phone);
    public Task<ResultDTO<int>> GetQuantity();
    public Task<ResultDTO<EmergencyContact>> Post(EmergencyContact emergencyContact);
    public Task<ResultDTO<EmergencyContact>> Put(EmergencyContact emergencyContact);
}
