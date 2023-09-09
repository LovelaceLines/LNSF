using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface IEmergencyContactRepository
{
    public Task<ResultDTO<List<EmergencyContact>>> Get(Pagination pagination);
    public Task<ResultDTO<EmergencyContact>> Get(int id);
    public Task<ResultDTO<int>> GetQuantity();
    public Task<ResultDTO<EmergencyContact>> Post(EmergencyContact emergencyContact);
    public Task<ResultDTO<EmergencyContact>> Put(EmergencyContact emergencyContact);
}
