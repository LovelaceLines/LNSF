using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;
public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<Patient> Create(Patient patient)
    {
        return await _patientRepository.Add(patient);
    }
    public async Task<Patient> Update(Patient patient)
    {
        return await _patientRepository.Update(patient);
    }
    public async Task<int> Count()
    {
        return await _patientRepository.GetCount();
    }
}