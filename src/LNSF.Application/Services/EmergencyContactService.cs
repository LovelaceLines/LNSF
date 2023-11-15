using LNSF.Application.Validators;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;
using LNSF.Application.Interfaces;

namespace LNSF.Application.Services;

public class EmergencyContactService : IEmergencyContactService
{
    private readonly IEmergencyContactRepository _emergencyContactRepository;
    private readonly IPeopleRepository _peoplesRepository;
    private readonly EmergencyContactValidator _validator;

    public EmergencyContactService(IEmergencyContactRepository emergencyContactsRepository,
        IPeopleRepository peoplesRepository,
        EmergencyContactValidator validator)
    {
        _emergencyContactRepository = emergencyContactsRepository;
        _peoplesRepository = peoplesRepository;
        _validator = validator;
    }

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilter filter) => 
        await _emergencyContactRepository.Query(filter);
    
    public async Task<int> GetCount() =>
        await _emergencyContactRepository.GetCount();

    public async Task<EmergencyContact> Create(EmergencyContact contact)
    {
        var validationResult = _validator.Validate(contact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _peoplesRepository.Exists(contact.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.UnprocessableEntity);

        return await _emergencyContactRepository.Add(contact);
    }

    public async Task<EmergencyContact> Update(EmergencyContact newContact)
    {
        var validationResult = _validator.Validate(newContact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _emergencyContactRepository.ExistsByIdAndPeopleId(newContact.Id, newContact.PeopleId)) throw new AppException("Contato de emergência ou pessoa não encontrado!", HttpStatusCode.UnprocessableEntity);

        return await _emergencyContactRepository.Update(newContact);
    }

    public async Task<EmergencyContact> Delete(int id)
    {
        if (!await _emergencyContactRepository.Exists(id)) throw new AppException("Contato de emergência não encontrado!", HttpStatusCode.UnprocessableEntity);

        return await _emergencyContactRepository.Remove(id);
    }
}
