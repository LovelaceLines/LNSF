﻿using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
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

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilters filters) => 
        await _emergencyContactRepository.Query(filters);
    
    public async Task<int> GetQuantity() =>
        await _emergencyContactRepository.GetQuantity();

    public async Task<EmergencyContact> Create(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _peoplesRepository.Get(contact.PeopleId);

        contact.Id = 0; // Garante que o contato será criado como novo.

        return await _emergencyContactRepository.Post(contact);
    }

    public async Task<EmergencyContact> Update(EmergencyContact contact)
    {
        var validationResult = _emergencyContactValidator.Validate(contact);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _peoplesRepository.Get(contact.PeopleId);

        var contactGet = await _emergencyContactRepository.Get(contact.Id);
        if (contactGet.PeopleId != contact.PeopleId) throw new AppException("Não é possível alterar o ID da pessoa.");

        return await _emergencyContactRepository.Put(contact);
    }

    public async Task<EmergencyContact> Delete(int id)
    {
        var contact = await _emergencyContactRepository.Get(id);
        return await _emergencyContactRepository.Delete(contact);
    }
}
