using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class EscortService : IEscortService
{
    private readonly IEscortRepository _escortRepository;
    private readonly IPeopleRepository _peoplesRepository;

    public EscortService(IEscortRepository escortRepository, 
        IPeopleRepository peoplesRepository)
    {
        _escortRepository = escortRepository;
        _peoplesRepository = peoplesRepository;
    }

    public async Task<List<Escort>> Query(EscortFilter filter) => 
        await _escortRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _escortRepository.GetCount();
        
    public async Task<Escort> Create(Escort escort)
    {
        if (!await _peoplesRepository.Exists(escort.PeopleId)) throw new AppException("Pessoa não encontrada", HttpStatusCode.NotFound);
        if (await _escortRepository.ExistsByPeopleId(escort.PeopleId)) throw new AppException("Acompanhante já cadastrado", HttpStatusCode.UnprocessableEntity);

        return await _escortRepository.Add(escort);
    }
    
    public async Task<Escort> Update(Escort escort)  
    {
        if (!await _escortRepository.ExistsByIdAndPeopleId(escort.Id, escort.PeopleId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

        return await _escortRepository.Update(escort);
    }

    public async Task<Escort> Delete(int id)
    {
        if (!await _escortRepository.Exists(id)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

        return await _escortRepository.Remove(id);
    }
}