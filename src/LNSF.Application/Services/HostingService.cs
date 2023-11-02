using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class HostingService : IHostingService
{
    private readonly IHostingRepository _hostingRepository;

    public HostingService(IHostingRepository hostingRepository) => 
        _hostingRepository = hostingRepository;

    public async Task<List<Hosting>> Query(HostingFilter filter) => 
        await _hostingRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _hostingRepository.GetCount();
        
    public async Task<Hosting> Create(Hosting hosting)
    {
        return await _hostingRepository.Add(hosting);
    }
    
    public async Task<Hosting> Update(Hosting hosting)
    {
        return await _hostingRepository.Update(hosting);
    }
}