using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PatientService(IPatientRepository repository,
	IHospitalRepository hospitalRepository,
	IPeopleRepository peopleRepository) : IPatientService
{
	public async Task<Patient> Create(Patient patient)
	{
		if (!await peopleRepository.ExistsById(patient.PeopleId)) throw new AppException("Pessoa não encontrada", HttpStatusCode.NotFound);
		if (await repository.ExistsByPeopleId(patient.PeopleId)) throw new AppException("Pessoa já cadastrada como paciente", HttpStatusCode.Conflict);
		if (!await hospitalRepository.ExistsById(patient.HospitalId)) throw new AppException("Hospital não encontrado", HttpStatusCode.NotFound);

		return await repository.Add(patient);
	}

	public async Task<Patient> Update(Patient patient)
	{
		if (!await repository.ExistsByIdAndPeopleId(patient.Id, patient.PeopleId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
		if (!await hospitalRepository.ExistsById(patient.HospitalId)) throw new AppException("Hospital não encontrado", HttpStatusCode.NotFound);

		return await repository.Update(patient);
	}

	public async Task<Patient> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);

		return await repository.RemoveById(id);
	}
}
