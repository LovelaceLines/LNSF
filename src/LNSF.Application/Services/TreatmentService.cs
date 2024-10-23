using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class TreatmentService(ITreatmentRepository repository,
	TreatmentValidator validator) : ITreatmentService
{
	public async Task<Treatment> Create(Treatment treatment)
	{
		var validationResult = await validator.ValidateAsync(treatment);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (await repository.ExistsByNameAndType(treatment.Name, treatment.Type)) throw new AppException("Tratamento já cadastrado", HttpStatusCode.Conflict);

		return await repository.Add(treatment);
	}

	public async Task<Treatment> Update(Treatment treatment)
	{
		var validationResult = await validator.ValidateAsync(treatment);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(treatment.Id)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
		if (await repository.ExistsByNameAndType(treatment.Name, treatment.Type)) throw new AppException("Tratamento já cadastrado", HttpStatusCode.Conflict);

		return await repository.Update(treatment);
	}

	public async Task<Treatment> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);

		return await repository.RemoveById(id);
	}
}
