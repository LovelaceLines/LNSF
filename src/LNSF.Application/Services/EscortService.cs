using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
namespace LNSF.Application.Services;
public class EscortService : IEscortService
{
    private readonly IEscortRepository _escortRepository;
    public EscortService(IEscortRepository escortRepository)
    {
        _escortRepository = escortRepository;
    }
    public async Task<List<Escort>> Query(EscortFilter filter)
    {
        return await _escortRepository.Query(filter);
    }
    public async Task<int> GetCount()
    {
        return await _escortRepository.GetCount();
    }
    public async Task<Escort> Create(Escort escort)
    {
        return await _escortRepository.Add(escort);
    }
    public async Task<Escort> Update(Escort escort)
    {
        return await _escortRepository.Update(escort);
    }
}