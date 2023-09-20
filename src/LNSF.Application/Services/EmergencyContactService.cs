using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class EmergencyContactService
{
    private readonly IEmergencyContactsRepository _emergencyContactRepository;
    private readonly EmergencyContactValidator _emergencyContactValidator;

    public EmergencyContactService(IEmergencyContactsRepository emergencyContactsRepository,
        IPeoplesRepository peoplesRepository,
        EmergencyContactValidator emergencyContactValidator)
    {
        _emergencyContactRepository = emergencyContactsRepository;
        _emergencyContactValidator = emergencyContactValidator;
    }

    public async Task<List<EmergencyContact>> Get(EmergencyContactFilters filters) => 
        await _emergencyContactRepository.Get(filters);

    public async Task<EmergencyContact> Get(int id) => 
        await _emergencyContactRepository.Get(id);
    
    public async Task<int> GetQuantity() =>
        await _emergencyContactRepository.GetQuantity();

    public async Task<EmergencyContact> CreateNewContact(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _emergencyContactRepository.Get(contact.PeopleId, contact.Phone);        

        contact.Id = 0; // Garante que o contato será criado como novo.

        return await _emergencyContactRepository.Post(contact);
    }

    public async Task<EmergencyContact> EditContact(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _emergencyContactRepository.Put(contact);
    }
}
