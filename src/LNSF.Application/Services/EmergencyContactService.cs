using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Domain.Views;

namespace LNSF.Application.Services;

public class EmergencyContactService
{
    private readonly IEmergencyContactRepository _emergencyContactRepository;
    private readonly PaginationValidator _paginationValidator;
    private readonly EmergencyContactValidator _emergencyContactValidator;

    public EmergencyContactService(IEmergencyContactRepository emergencyContactRepository,
        PaginationValidator paginationValidator,
        EmergencyContactValidator emergencyContactValidator)
    {
        _emergencyContactRepository = emergencyContactRepository;
        _paginationValidator = paginationValidator;
        _emergencyContactValidator = emergencyContactValidator;
    }

    public async Task<ResultDTO<List<EmergencyContact>>> Get(Pagination pagination)
    {
        var validationResult = _paginationValidator.Validate(pagination);

        return validationResult.IsValid ?
            await _emergencyContactRepository.Get(pagination) :
            new ResultDTO<List<EmergencyContact>>(validationResult.ToString());
    }

    public async Task<ResultDTO<EmergencyContact>> Get(int id) => 
        await _emergencyContactRepository.Get(id);
    
    public async Task<ResultDTO<int>> GetQuantity() =>
        await _emergencyContactRepository.GetQuantity();

    public async Task<ResultDTO<EmergencyContact>> Post(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);

        return validationResult.IsValid ?
            await _emergencyContactRepository.Post(contact) :
            new ResultDTO<EmergencyContact>(validationResult.ToString());
    }

    public async Task<ResultDTO<EmergencyContact>> Put(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);

        return validationResult.IsValid ?
            await _emergencyContactRepository.Put(contact) :
            new ResultDTO<EmergencyContact>(validationResult.ToString());
    }

}
