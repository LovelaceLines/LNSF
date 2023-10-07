using LNSF.Application.Validators;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

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

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilter filter) => 
        await _emergencyContactRepository.Query(filter);
    
    public async Task<int> GetCount() =>
        await _emergencyContactRepository.GetCount();

    public async Task<EmergencyContact> Create(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (!await _peoplesRepository.Exists(contact.PeopleId)) throw new AppException("Pessoa não encontrada.", HttpStatusCode.UnprocessableEntity);

        contact.Id = 0;
        return await _emergencyContactRepository.Add(contact);
    }

    public async Task<EmergencyContact> Update(EmergencyContact newContact)
    {
        var validationResult = _emergencyContactValidator.Validate(newContact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        var filter = new EmergencyContactFilter { Id = newContact.Id, PeopleId = newContact.PeopleId };
        var query = await _emergencyContactRepository.Query(filter);
        if (query.Count != 1) throw new AppException("Contato de emergência não encontrado.", HttpStatusCode.UnprocessableEntity);

        return await _emergencyContactRepository.Update(newContact);
    }

    public async Task<EmergencyContact> Delete(int id)
    {
        if (!await _emergencyContactRepository.Exists(id)) throw new AppException("Contato de emergência não encontrado.", HttpStatusCode.UnprocessableEntity);

        return await _emergencyContactRepository.Remove(id);
    }
}
