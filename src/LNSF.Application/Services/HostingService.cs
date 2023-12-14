using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class HostingService : IHostingService
{
    private readonly IHostingRepository _hostingRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly HostingValidator _validator;

    public HostingService(IHostingRepository hostingRepository,
        HostingValidator validator,
        IPatientRepository patientRepository)
    {
        _hostingRepository = hostingRepository;
        _validator = validator;
        _patientRepository = patientRepository;
    }

    public async Task<List<Hosting>> Query(HostingFilter filter) => 
        await _hostingRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _hostingRepository.GetCount();
        
    public async Task<Hosting> Create(Hosting hosting)
    {
        var validationResult = await _validator.ValidateAsync(hosting);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _patientRepository.ExistsById(hosting.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
        if (await _hostingRepository.ExistsByPatientIdAndCheckInAndCheckOut(hosting)) throw new AppException("Já existe uma hospedagem para este paciente neste período", HttpStatusCode.Conflict);
        
        return await _hostingRepository.Add(hosting);
    }

    public async Task<Hosting> Update(Hosting hosting)
    {
        if (!await _hostingRepository.ExistsByIdAndPatientId(hosting.Id, hosting.PatientId)) throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);

        var validationResult = await _validator.ValidateAsync(hosting);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (await _hostingRepository.ExistsByPatientIdAndCheckInAndCheckOut(hosting)) throw new AppException("Já existe uma hospedagem para este paciente neste período", HttpStatusCode.Conflict);

        return await _hostingRepository.Update(hosting);
    }
}