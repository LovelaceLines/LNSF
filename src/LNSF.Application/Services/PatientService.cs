using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IPeopleRepository _peopleRepository;

    public PatientService(IPatientRepository patientRepository,
        IHospitalRepository hospitalRepository,
        IPeopleRepository peopleRepository)
    {
        _patientRepository = patientRepository;
        _hospitalRepository = hospitalRepository;
        _peopleRepository = peopleRepository;
    }

    public async Task<List<Patient>> Query(PatientFilter filter) => 
        await _patientRepository.Query(filter);

    public async Task<int> Count() => 
        await _patientRepository.GetCount();

    public async Task<Patient> Create(Patient patient)
    {
        if (!await _peopleRepository.ExistsById(patient.PeopleId)) throw new AppException("Pessoa não encontrada", HttpStatusCode.NotFound);
        if (await _patientRepository.ExistsByPeopleId(patient.PeopleId)) throw new AppException("Pessoa já cadastrada como paciente", HttpStatusCode.Conflict);
        if (!await _hospitalRepository.ExistsById(patient.HospitalId)) throw new AppException("Hospital não encontrado", HttpStatusCode.NotFound);
        
        return await _patientRepository.Add(patient);
    }
    
    public async Task<Patient> Update(Patient patient)
    {
        if (!await _patientRepository.ExistsByIdAndPeopleId(patient.Id, patient.PeopleId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
        if (!await _hospitalRepository.ExistsById(patient.HospitalId)) throw new AppException("Hospital não encontrado", HttpStatusCode.NotFound);
        
        return await _patientRepository.Update(patient);
    }

    public async Task<Patient> Delete(int id)
    {
        if (!await _patientRepository.ExistsById(id)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);

        return await _patientRepository.RemoveById(id);
    }
}