using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
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
        return await _treatmentRepository.Add(treatment);
    }
    
     public async Task<Treatment> Update(Treatment treatment) 
    {
        return await _treatmentRepository.Update(treatment);
    }
}