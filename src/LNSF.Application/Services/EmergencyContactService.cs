using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Domain.Views;

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
        contact.Id = 0;

        var people = await _peoplesRepository.Get(contact.PeopleId);

        if (people.Error == true) return new ResultDTO<EmergencyContact>("Pessoa não encontrada.");

        var validationResult = _emergencyContactValidator.Validate(contact);

        return validationResult.IsValid ?
            await _emergencyContactRepository.Post(contact) :
            new ResultDTO<EmergencyContact>(validationResult.ToString());
    }

    public async Task<ResultDTO<EmergencyContact>> Put(EmergencyContact contact)
    {
        if (contact.Id == 0) return new ResultDTO<EmergencyContact>("Contato não encontrado.");
        if (contact.PeopleId == 0) return new ResultDTO<EmergencyContact>("Pessoa não encontrada.");

        // var people = await _peoplesRepository.Get(contact.PeopleId);
        
        // if (people.Error == true) return new ResultDTO<EmergencyContact>("Pessoa não encontrada.");

        var validationResult = _emergencyContactValidator.Validate(contact);

        return validationResult.IsValid ?
            await _emergencyContactRepository.Put(contact) :
            new ResultDTO<EmergencyContact>(validationResult.ToString());
    }
}
