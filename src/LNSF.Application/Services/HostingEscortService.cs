using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class HostingEscortService : IHostingEscortService
{
    private readonly IHostingEscortRepository _repository;

    public HostingEscortService(IHostingEscortRepository repository) => 
        _repository = repository;

    public async Task<List<HostingEscort>> Query(HostingEscortFilter filter) => 
        await _repository.Query(filter);

    public async Task<int> GetCount() => 
        await _repository.GetCount();
}
