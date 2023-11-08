using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class TreatmentService : ITreatmentService
{
    private readonly ITreatmentRepository _treatmentRepository;
    private readonly TreatmentValidator _validator;

    public TreatmentService(ITreatmentRepository treatmentRepository, 
        TreatmentValidator validator)
    {
        _treatmentRepository = treatmentRepository;
        _validator = validator;
    }

    public async Task<List<Treatment>> Query(TreatmentFilter filter) => 
        await _treatmentRepository.Query(filter);
    
    public async Task<int> GetCount() => 
        await _treatmentRepository.GetCount();
    
    public async Task<Treatment> Create(Treatment treatment)
    {
        var validationResult = await _validator.ValidateAsync(treatment);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (await _treatmentRepository.ExistsByNameAndType(treatment.Name, treatment.Type)) throw new AppException("Tratamento já cadastrado", HttpStatusCode.BadRequest);

        return await _treatmentRepository.Add(treatment);
    }
    
    public async Task<Treatment> Update(Treatment treatment) 
    {
        var validationResult = await _validator.ValidateAsync(treatment);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _treatmentRepository.Exists(treatment.Id)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
        if (await _treatmentRepository.ExistsByNameAndType(treatment.Name, treatment.Type)) throw new AppException("Tratamento já cadastrado", HttpStatusCode.BadRequest);


        return await _treatmentRepository.Update(treatment);
    }

    public async Task<Treatment> Delete(int id)
    {
        if (!await _treatmentRepository.Exists(id)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);

        return await _treatmentRepository.Remove(id);
    }
}