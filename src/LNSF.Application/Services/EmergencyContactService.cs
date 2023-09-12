using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class EmergencyContactService
{
    private readonly IEmergencyContactsRepository _emergencyContactRepository;
    private readonly IPeoplesRepository _peoplesRepository;
    private readonly PaginationValidator _paginationValidator;
    private readonly EmergencyContactValidator _emergencyContactValidator;

    public EmergencyContactService(IEmergencyContactsRepository emergencyContactsRepository,
        IPeoplesRepository peoplesRepository,
        PaginationValidator paginationValidator,
        EmergencyContactValidator emergencyContactValidator)
    {
        _emergencyContactRepository = emergencyContactsRepository;
        _peoplesRepository = peoplesRepository;
        _paginationValidator = paginationValidator;
        _emergencyContactValidator = emergencyContactValidator;
    }

    public async Task<ResultDTO<List<EmergencyContact>>> Get() => 
        await _emergencyContactRepository.Get();

    public async Task<ResultDTO<EmergencyContact>> Get(int id) => 
        await _emergencyContactRepository.Get(id);
    
    public async Task<ResultDTO<int>> GetQuantity() =>
        await _emergencyContactRepository.GetQuantity();

    public async Task<ResultDTO<EmergencyContact>> CreateNewContact(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);

        var people = await _peoplesRepository.Get(contact.PeopleId);

        if (people.Error == true) return new ResultDTO<EmergencyContact>("Pessoa não encontrada.");
        
        var contactFound = await _emergencyContactRepository.Get(contact.PeopleId, contact.Phone);
        if (contactFound.Error == false) return new ResultDTO<EmergencyContact>("Contato já cadastrado.");

        contact.Id = 0; // Garante que o contato será criado como novo.

        return validationResult.IsValid ?
            await _emergencyContactRepository.Post(contact) :
            new ResultDTO<EmergencyContact>(validationResult.ToString());
    }

    public async Task<ResultDTO<EmergencyContact>> EditContact(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);

        var resultContact = await _emergencyContactRepository.Get(contact.Id);
        if (resultContact.Error == true) return new ResultDTO<EmergencyContact>("Contato não encontrado.");

        return validationResult.IsValid ?
            await _emergencyContactRepository.Put(contact) :
            new ResultDTO<EmergencyContact>(validationResult.ToString());
    }
}
