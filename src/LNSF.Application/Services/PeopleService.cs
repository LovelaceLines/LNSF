using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace LNSF.Application.Services;

public class PeopleService(IPeopleRepository repository, PeopleValidator validator) : IPeopleService
{
	public async Task<People> Create(People people)
	{
		var validationResult = validator.Validate(people);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (await repository.ExistsByCpf(people.CPF)) throw new AppException("CPF já cadastrado!", HttpStatusCode.Conflict);
		if (await repository.ExistsByRg(people.RG)) throw new AppException("RG já cadastrado!", HttpStatusCode.Conflict);
		if (!people.Email.IsNullOrEmpty() && await repository.ExistsByEmail(people.Email!)) throw new AppException("Email já cadastrado!", HttpStatusCode.Conflict);
		if (!people.Phone.IsNullOrEmpty() && await repository.ExistsByPhone(people.Phone!)) throw new AppException("Telefone já cadastrado!", HttpStatusCode.Conflict);

		return await repository.Add(people);
	}

	public async Task<People> Update(People newPeople)
	{
		var validationResult = validator.Validate(newPeople);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(newPeople.Id)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
		var oldPeople = await repository.GetById(newPeople.Id);

		if (oldPeople.CPF != newPeople.CPF && await repository.ExistsByCpf(newPeople.CPF)) throw new AppException("CPF já cadastrado!", HttpStatusCode.Conflict);
		if (oldPeople.RG != newPeople.RG && await repository.ExistsByRg(newPeople.RG)) throw new AppException("RG já cadastrado!", HttpStatusCode.Conflict);
		if (oldPeople.Email != newPeople.Email && !newPeople.Email.IsNullOrEmpty() && await repository.ExistsByEmail(newPeople.Email!)) throw new AppException("Email já cadastrado!", HttpStatusCode.Conflict);
		if (oldPeople.Phone != newPeople.Phone && !newPeople.Phone.IsNullOrEmpty() && await repository.ExistsByPhone(newPeople.Phone!)) throw new AppException("Telefone já cadastrado!", HttpStatusCode.Conflict);

		return await repository.Update(newPeople);
	}
}
