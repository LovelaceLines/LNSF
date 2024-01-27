using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class ServiceRecordService : IServiceRecordService
{
    private readonly IServiceRecordRepository _repository;
    private readonly IPatientRepository _patientRepository;

    public ServiceRecordService(IServiceRecordRepository serviceRecordRepository,
        IPatientRepository patientRepository)
    {
        _repository = serviceRecordRepository;
        _patientRepository = patientRepository;
    }

    public Task<List<ServiceRecord>> Query(ServiceRecordFilter filter) =>
        _repository.Query(filter);

    public Task<int> GetCount() =>
        _repository.GetCount();

    public async Task<ServiceRecord> Create(ServiceRecord serviceRecord)
    {
        if (!await _patientRepository.ExistsById(serviceRecord.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
        if (await _repository.ExistsByPatientId(serviceRecord.PatientId)) throw new AppException("Paciente já possui um protuário", HttpStatusCode.Conflict);

        return await _repository.Add(serviceRecord);
    }

    public async Task<ServiceRecord> Update(ServiceRecord newServiceRecord)
    {
        var oldServiceRecord = await _repository.GetById(newServiceRecord.Id);
        if (oldServiceRecord.PatientId != newServiceRecord.PatientId && await _repository.ExistsById(newServiceRecord.Id, newServiceRecord.PatientId)) throw new AppException("Paciente já possui um protuário", HttpStatusCode.Conflict);
        return await _repository.Update(newServiceRecord);
    }

    public async Task<ServiceRecord> Delete(int id)
    {
        if (!await _repository.ExistsById(id)) throw new AppException("Protuário não encontrado", HttpStatusCode.NotFound);
        return await _repository.RemoveById(id);
    }
}
