using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class EscortService : IEscortService
{
    private readonly IEscortRepository _escortRepository;

    public EscortService(IEscortRepository escortRepository) => 
        _escortRepository = escortRepository;

    public async Task<List<Escort>> Query(EscortFilter filter) => 
        await _escortRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _escortRepository.GetCount();
        
    public async Task<Escort> Create(Escort escort)
    {
        return await _escortRepository.Add(escort);
    }
    
    public async Task<Escort> Update(Escort escort)
    {
        return await _escortRepository.Update(escort);
    }
}