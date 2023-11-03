using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class TreatmentService : ITreatmentService
{
    private readonly ITreatmentRepository _treatmentRepository;
    
    public TreatmentService(ITreatmentRepository treatmentRepository) => 
        _treatmentRepository = treatmentRepository;

    public async Task<List<Treatment>> Query(TreatmentFilter filter) => 
        await _treatmentRepository.Query(filter);
    
    public async Task<int> GetCount() => 
        await _treatmentRepository.GetCount();
    
    public async Task<Treatment> Create(Treatment treatment)
    {
        if (await _treatmentRepository.NameExists(treatment.Name)) throw new AppException("Tratamento já cadastrado", HttpStatusCode.BadRequest);

        return await _treatmentRepository.Add(treatment);
    }
    
     public async Task<Treatment> Update(Treatment treatment) 
    {
        if (!await _treatmentRepository.Exists(treatment.Id)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
        var oldTraetment = await _treatmentRepository.Get(treatment.Id);
        if (oldTraetment.Name != treatment.Name && await _treatmentRepository.NameExists(treatment.Name)) throw new AppException("Tratamento já cadastrado", HttpStatusCode.BadRequest);

        return await _treatmentRepository.Update(treatment);
    }
}