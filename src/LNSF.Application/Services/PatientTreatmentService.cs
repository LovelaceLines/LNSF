using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class PatientTreatmentService : IPatientTreatmentService
{
    private readonly IPatientTreatmentRepository _repository;

    public PatientTreatmentService(IPatientTreatmentRepository repository) => 
        _repository = repository;

    public Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter) => 
        _repository.Query(filter);

    public async Task<int> GetCount() => 
        await _repository.GetCount();
}
