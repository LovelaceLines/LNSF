using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class EmergencyContactService
{
    private readonly IEmergencyContactsRepository _emergencyContactRepository;
    private readonly IPeoplesRepository _peoplesRepository;
    private readonly EmergencyContactValidator _emergencyContactValidator;

    public EmergencyContactService(IEmergencyContactsRepository emergencyContactsRepository,
        IPeoplesRepository peoplesRepository,
        EmergencyContactValidator emergencyContactValidator)
    {
        _emergencyContactRepository = emergencyContactsRepository;
        _peoplesRepository = peoplesRepository;
        _emergencyContactValidator = emergencyContactValidator;
    }

    public async Task<ResultDTO<List<EmergencyContact>>> Get(EmergencyContactFilters filters) => 
        await _emergencyContactRepository.Get(filters);

    public async Task<ResultDTO<EmergencyContact>> Get(int id) => 
        await _emergencyContactRepository.Get(id);
    
    public async Task<ResultDTO<int>> GetQuantity() =>
        await _emergencyContactRepository.GetQuantity();

    public async Task<ResultDTO<EmergencyContact>> CreateNewContact(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) return new ResultDTO<EmergencyContact>(validationResult.ToString());

        var people = await _peoplesRepository.Get(contact.PeopleId);
        if (people.Error == true) return new ResultDTO<EmergencyContact>("Pessoa não encontrada.");
        
        var contactFound = await _emergencyContactRepository.Get(contact.PeopleId, contact.Phone);
        if (contactFound.Error == false) return new ResultDTO<EmergencyContact>("Contato já cadastrado.");

        contact.Id = 0; // Garante que o contato será criado como novo.

        return await _emergencyContactRepository.Post(contact);
    }

    public async Task<ResultDTO<EmergencyContact>> EditContact(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) return new ResultDTO<EmergencyContact>(validationResult.ToString());

        var resultContact = await _emergencyContactRepository.Get(contact.Id);
        if (resultContact.Error == true) return new ResultDTO<EmergencyContact>("Contato não encontrado.");

        return await _emergencyContactRepository.Put(contact);
    }
}
