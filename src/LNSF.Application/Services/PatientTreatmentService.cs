using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PatientTreatmentService : IPatientTreatmentService
{
    private readonly IPatientTreatmentRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly ITreatmentRepository _treatmentRepository;

    public PatientTreatmentService(IPatientTreatmentRepository repository, 
        IPatientRepository patientRepository, 
        ITreatmentRepository treatmentRepository)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _treatmentRepository = treatmentRepository;
    }

    public Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter) => 
        _repository.Query(filter);

    public async Task<int> GetCount() => 
        await _repository.GetCount();
    
    public async Task<PatientTreatment> Create(PatientTreatment patientTreatment)
    {
        if (!await _patientRepository.ExistsById(patientTreatment.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
        if (!await _treatmentRepository.ExistsById(patientTreatment.TreatmentId)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
        if (await _repository.ExistsByPatientIdAndTreatmentId(patientTreatment.PatientId, patientTreatment.TreatmentId)) throw new AppException("Tratamento já está vinculado a este paciente", HttpStatusCode.Conflict);

        return await _repository.Add(patientTreatment);
    }

    public async Task<PatientTreatment> Delete(int patientId, int treatmentId)
    {
        if (!await _repository.ExistsByPatientIdAndTreatmentId(patientId, treatmentId)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);

        var patientTreatment = await _repository.GetByPatientIdAndTreatmentId(patientId, treatmentId);
        
        return await _repository.Remove(patientTreatment);
    }
}
