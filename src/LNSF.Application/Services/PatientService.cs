using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly ITreatmentRepository _treatmentRepository;

    public PatientService(IPatientRepository patientRepository,
        IHospitalRepository hospitalRepository,
        IPeopleRepository peopleRepository,
        ITreatmentRepository treatmentRepository)
    {
        _patientRepository = patientRepository;
        _hospitalRepository = hospitalRepository;
        _peopleRepository = peopleRepository;
        _treatmentRepository = treatmentRepository;
    }

    public async Task<List<Patient>> Query(PatientFilter filter) => 
        await _patientRepository.Query(filter);

    public async Task<int> Count() => 
        await _patientRepository.GetCount();

    public async Task<Patient> Create(Patient patient)
    {
        if (!await _hospitalRepository.Exists(patient.HospitalId)) throw new AppException("Hospital não encontrado", HttpStatusCode.NotFound);
        if (patient.TreatmentIds.Count == 0) throw new AppException("Pelo menos um tratamento deve ser informado", HttpStatusCode.BadRequest);
        foreach (var t in patient.TreatmentIds)
            if (!await _treatmentRepository.Exists(t)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
        if (!await _peopleRepository.Exists(patient.PeopleId)) throw new AppException("Pessoa não encontrada", HttpStatusCode.NotFound);
        if (await _patientRepository.PeopleExists(patient.PeopleId)) throw new AppException("Pessoa já cadastrada como paciente", HttpStatusCode.BadRequest);

        return await _patientRepository.Add(patient);
    }
    
    public async Task<Patient> Update(Patient patient)
    {
        if (!await _patientRepository.Exists(patient.Id)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
        if (!await _hospitalRepository.Exists(patient.HospitalId)) throw new AppException("Hospital não encontrado", HttpStatusCode.NotFound);
        if (patient.TreatmentIds.Count == 0) throw new AppException("Pelo menos um tratamento deve ser informado", HttpStatusCode.BadRequest);
        foreach (var t in patient.TreatmentIds)
            if (!await _treatmentRepository.Exists(t)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
        if (!await _peopleRepository.Exists(patient.PeopleId)) throw new AppException("Pessoa não encontrada", HttpStatusCode.NotFound);
        var oldPatient = await _patientRepository.Get(patient.Id);
        if (oldPatient.PeopleId != patient.PeopleId && await _patientRepository.PeopleExists(patient.PeopleId)) throw new AppException("Pessoa já cadastrada como paciente", HttpStatusCode.BadRequest);

        return await _patientRepository.Update(patient);
    }
}