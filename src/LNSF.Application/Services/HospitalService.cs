using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class HospitalService(IHospitalRepository repository,
	HospitalValidator validator) : IHospitalService
{
	public async Task<Hospital> Create(Hospital hospital)
	{
		var validationResult = await validator.ValidateAsync(hospital);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (await repository.ExistsByName(hospital.Name)) throw new AppException("Nome do hospital já cadastrado!", HttpStatusCode.Conflict);

		return await repository.Add(hospital);
	}

	public async Task<Hospital> Update(Hospital newHospital)
	{
		var validationResult = await validator.ValidateAsync(newHospital);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(newHospital.Id)) throw new AppException("Hospital não encontrado!", HttpStatusCode.NotFound);
		var oldHospital = await repository.GetById(newHospital.Id);
		if (oldHospital.Name != newHospital.Name && await repository.ExistsByName(newHospital.Name)) throw new AppException("Nome do hospital já cadastrado!", HttpStatusCode.Conflict);

		return await repository.Update(newHospital);
	}

	public async Task<Hospital> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Hospital não encontrado!", HttpStatusCode.NotFound);

		return await repository.RemoveById(id);
	}
}
