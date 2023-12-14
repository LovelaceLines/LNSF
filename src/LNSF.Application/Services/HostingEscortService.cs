using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class HostingEscortService : IHostingEscortService
{
    private readonly IHostingEscortRepository _repository;
    private readonly IHostingRepository _hostingRepository;
    private readonly IEscortRepository _escortRepository;

    public HostingEscortService(IHostingEscortRepository repository,
        IHostingRepository hostingRepository,
        IEscortRepository escortRepository)
    {
        _repository = repository;
        _hostingRepository = hostingRepository;
        _escortRepository = escortRepository;
    }

    public async Task<List<HostingEscort>> Query(HostingEscortFilter filter) => 
        await _repository.Query(filter);

    public async Task<int> GetCount() => 
        await _repository.GetCount();

    public async Task<HostingEscort> Create(HostingEscort hostingEscort)
    {
        if (!await _escortRepository.ExistsById(hostingEscort.EscortId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);
        if (!await _hostingRepository.ExistsById(hostingEscort.HostingId)) throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);
        if (await _repository.ExistsByHostingIdAndEscortId(hostingEscort.HostingId, hostingEscort.EscortId)) throw new AppException("Acompanhante já está vinculado a esta hospedagem", HttpStatusCode.Conflict);
        if (await _repository.ExistsByEscortIdAndCheckInAndCheckOut(hostingEscort.HostingId, hostingEscort.EscortId)) throw new AppException("Já existe uma hospedagem para este acompanhante neste período", HttpStatusCode.Conflict);

        return await _repository.Add(hostingEscort);
    }

    public async Task<HostingEscort> Delete(int hostingId, int escortId)
    {
        if (!await _repository.ExistsByHostingIdAndEscortId(hostingId, escortId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

        var hostingEscort = await _repository.GetByHostingIdAndEscortId(hostingId, escortId);
        
        return await _repository.Remove(hostingEscort);
    }
}
