using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class FamilyGroupProfileService(IFamilyGroupProfileRepository repository,
	FamilyGroupProfileValidator validator,
	IPatientRepository patientRepository) : IFamilyGroupProfileService
{
	public async Task<FamilyGroupProfile> Create(FamilyGroupProfile familyGroupProfile)
	{
		var validationResult = await validator.ValidateAsync(familyGroupProfile);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await patientRepository.ExistsById(familyGroupProfile.PatientId)) throw new AppException("Paciente não encontrado!", HttpStatusCode.NotFound);

		return await repository.Add(familyGroupProfile);
	}

	public async Task<FamilyGroupProfile> Update(FamilyGroupProfile familyGroupProfile)
	{
		var validationResult = await validator.ValidateAsync(familyGroupProfile);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(familyGroupProfile.Id)) throw new AppException("Perfil de grupo familiar não encontrado!", HttpStatusCode.NotFound);
		if (!await patientRepository.ExistsById(familyGroupProfile.PatientId)) throw new AppException("Paciente não encontrado!", HttpStatusCode.NotFound);

		return await repository.Update(familyGroupProfile);
	}

	public async Task<FamilyGroupProfile> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Perfil de grupo familiar não encontrado!", HttpStatusCode.NotFound);

		return await repository.RemoveById(id);
	}
}
