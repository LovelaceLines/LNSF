using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class HostingService(IHostingRepository repository,
	HostingValidator validator,
	IPatientRepository patientRepository) : IHostingService
{
	public async Task<Hosting> Create(Hosting hosting)
	{
		var validationResult = await validator.ValidateAsync(hosting);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await patientRepository.ExistsById(hosting.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
		if (await repository.ExistsWithDateConflict(hosting)) throw new AppException("Já existe uma hospedagem para este paciente neste período", HttpStatusCode.Conflict);

		return await repository.Add(hosting);
	}

	public async Task<Hosting> Update(Hosting hosting)
	{
		if (!await repository.ExistsByIdAndPatientId(hosting.Id, hosting.PatientId)) throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);

		var validationResult = await validator.ValidateAsync(hosting);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (await repository.ExistsWithDateConflict(hosting)) throw new AppException("Já existe uma hospedagem para este paciente neste período", HttpStatusCode.Conflict);

		return await repository.Update(hosting);
	}
}
