using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository) => 
        _patientRepository = patientRepository;

    public async Task<List<Patient>> Query(PatientFilter filter) => 
        await _patientRepository.Query(filter);

    public async Task<int> Count() => 
        await _patientRepository.GetCount();

    public async Task<Patient> Create(Patient patient)
    {
        return await _patientRepository.Add(patient);
    }
    
    public async Task<Patient> Update(Patient patient)
    {
        return await _patientRepository.Update(patient);
    }
}