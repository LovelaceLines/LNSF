using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PatientTreatmentService(IPatientTreatmentRepository repository,
	IPatientRepository patientRepository,
	ITreatmentRepository treatmentRepository) : IPatientTreatmentService
{
	public async Task<PatientTreatment> Create(PatientTreatment patientTreatment)
	{
		if (!await patientRepository.ExistsById(patientTreatment.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
		if (!await treatmentRepository.ExistsById(patientTreatment.TreatmentId)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);
		if (await repository.ExistsByPatientIdAndTreatmentId(patientTreatment.PatientId, patientTreatment.TreatmentId)) throw new AppException("Tratamento já está vinculado a este paciente", HttpStatusCode.Conflict);

		return await repository.Add(patientTreatment);
	}

	public async Task<PatientTreatment> Delete(PatientTreatment patientTreatment)
	{
		if (!await repository.ExistsByPatientIdAndTreatmentId(patientTreatment.PatientId, patientTreatment.TreatmentId)) throw new AppException("Tratamento não encontrado", HttpStatusCode.NotFound);

		var patientTreatmentToDelete = await repository.GetByPatientIdAndTreatmentId(patientTreatment.PatientId, patientTreatment.TreatmentId);

		return await repository.Remove(patientTreatmentToDelete);
	}
}
