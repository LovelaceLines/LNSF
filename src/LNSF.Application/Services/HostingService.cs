using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class HostingService : IHostingService
{
    private readonly IHostingRepository _hostingRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly HostingValidator _validator;

    public HostingService(IHostingRepository hostingRepository,
        HostingValidator validator,
        IPeopleRepository peopleRepository)
    {
        _hostingRepository = hostingRepository;
        _validator = validator;
        _peopleRepository = peopleRepository;
    }

    public async Task<List<Hosting>> Query(HostingFilter filter) => 
        await _hostingRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _hostingRepository.GetCount();
        
    public async Task<Hosting> Create(Hosting hosting)
    {
        var validationResult = await _validator.ValidateAsync(hosting);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _peopleRepository.Exists(hosting.PatientId)) throw new AppException("Paciente não encontrado", HttpStatusCode.NotFound);
        foreach (var escortId in hosting.EscortsIds)
            if (!await _peopleRepository.Exists(escortId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

        return await _hostingRepository.Add(hosting);
    }

    public async Task<Hosting> Update(Hosting hosting)
    {
        if (!await _hostingRepository.Exists(hosting.Id)) throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);

        var validationResult = await _validator.ValidateAsync(hosting);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        var oldHosting = await _hostingRepository.Get(hosting.Id);
        if (oldHosting.PatientId != hosting.PatientId) throw new AppException("Não é possível alterar o paciente da hospedagem", HttpStatusCode.BadRequest);
        foreach (var escortId in hosting.EscortsIds)
            if (!await _peopleRepository.Exists(escortId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

        return await _hostingRepository.Update(hosting);
    }
}