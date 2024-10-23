using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class ServiceRecordService(IServiceRecordRepository repository,
	IPatientRepository patientRepository) : IServiceRecordService
{
	public async Task<ServiceRecord> Create(ServiceRecord serviceRecord)
	{
		if (!await patientRepository.ExistsById(serviceRecord.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
		if (await repository.ExistsByPatientId(serviceRecord.PatientId)) throw new AppException("Paciente já possui um protuário", HttpStatusCode.Conflict);

		return await repository.Add(serviceRecord);
	}

	public async Task<ServiceRecord> Update(ServiceRecord newServiceRecord)
	{
		var oldServiceRecord = await repository.GetById(newServiceRecord.Id);
		if (oldServiceRecord.PatientId != newServiceRecord.PatientId && await repository.ExistsById(newServiceRecord.Id, newServiceRecord.PatientId)) throw new AppException("Paciente já possui um protuário", HttpStatusCode.Conflict);
		return await repository.Update(newServiceRecord);
	}

	public async Task<ServiceRecord> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Protuário não encontrado", HttpStatusCode.NotFound);
		return await repository.RemoveById(id);
	}
}
