using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Application.Interfaces;
using LNSF.Domain.Repositories;
namespace LNSF.Application.Services;

public class TreatmentService : ITreatmentService
 {
     private readonly ITreatmentRepository _treatmentRepository;
     public TreatmentService(ITreatmentRepository treatmentRepository)
     {
         _treatmentRepository = treatmentRepository;
     }
    public async Task<List<Treatment>> Query(TreatmentFilter filter)
    {
        return await _treatmentRepository.Query(filter);
    }
     public async Task<Treatment> Create(Treatment treatment)
     {
         return await _treatmentRepository.Add(treatment);
     }
     public async Task<Treatment> Update(Treatment treatment) 
     {
         return await _treatmentRepository.Update(treatment);
     }
     public async Task<int> GetCount()
     {
         return await _treatmentRepository.GetCount();
     }
 }