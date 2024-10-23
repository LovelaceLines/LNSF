using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class EmergencyContactService(IEmergencyContactRepository repository,
	IPeopleRepository peopleRepository,
	EmergencyContactValidator validator) : IEmergencyContactService
{
	public async Task<EmergencyContact> Create(EmergencyContact contact)
	{
		var validationResult = validator.Validate(contact);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await peopleRepository.ExistsById(contact.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);

		return await repository.Add(contact);
	}

	public async Task<EmergencyContact> Update(EmergencyContact newContact)
	{
		var validationResult = validator.Validate(newContact);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsByIdAndPeopleId(newContact.Id, newContact.PeopleId)) throw new AppException("Contato de emergência ou pessoa não encontrado!", HttpStatusCode.NotFound);

		return await repository.Update(newContact);
	}

	public async Task<EmergencyContact> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Contato de emergência não encontrado!", HttpStatusCode.NotFound);

		return await repository.RemoveById(id);
	}
}
