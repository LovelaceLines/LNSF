using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class FamilyGroupProfileService : IFamilyGroupProfileService
{
    private readonly IFamilyGroupProfileRepository _familyGroupProfileRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly FamilyGroupProfileValidator _validator;

    public FamilyGroupProfileService(IFamilyGroupProfileRepository familyGroupProfileRepository,
        FamilyGroupProfileValidator validator,
        IPatientRepository patientRepository)
    {
        _familyGroupProfileRepository = familyGroupProfileRepository;
        _validator = validator;
        _patientRepository = patientRepository;
    }

    public Task<List<FamilyGroupProfile>> Query(FamilyGroupProfileFilter filter) =>
        _familyGroupProfileRepository.Query(filter);

    public Task<int> GetCount() =>
        _familyGroupProfileRepository.GetCount();

    public async Task<FamilyGroupProfile> Create(FamilyGroupProfile familyGroupProfile)
    {
        var validationResult = await _validator.ValidateAsync(familyGroupProfile);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _patientRepository.ExistsById(familyGroupProfile.PatientId)) throw new AppException("Paciente não encontrado!", HttpStatusCode.NotFound);

        return await _familyGroupProfileRepository.Add(familyGroupProfile);
    }

    public async Task<FamilyGroupProfile> Update(FamilyGroupProfile familyGroupProfile)
    {
        var validationResult = await _validator.ValidateAsync(familyGroupProfile);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _familyGroupProfileRepository.ExistsById(familyGroupProfile.Id)) throw new AppException("Perfil de grupo familiar não encontrado!", HttpStatusCode.NotFound);
        if (!await _patientRepository.ExistsById(familyGroupProfile.PatientId)) throw new AppException("Paciente não encontrado!", HttpStatusCode.NotFound);

        return await _familyGroupProfileRepository.Update(familyGroupProfile);
    }

    public async Task<FamilyGroupProfile> Delete(int id)
    {
        if (!await _familyGroupProfileRepository.ExistsById(id)) throw new AppException("Perfil de grupo familiar não encontrado!", HttpStatusCode.NotFound);

        return await _familyGroupProfileRepository.RemoveById(id);
    }
}
