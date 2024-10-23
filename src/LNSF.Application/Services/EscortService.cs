using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class EscortService(IEscortRepository repository,
	IPeopleRepository peopleRepository) : IEscortService
{
	public async Task<Escort> Create(Escort escort)
	{
		if (!await peopleRepository.ExistsById(escort.PeopleId)) throw new AppException("Pessoa não encontrada", HttpStatusCode.NotFound);
		if (await repository.ExistsByPeopleId(escort.PeopleId)) throw new AppException("Acompanhante já cadastrado", HttpStatusCode.Conflict);

		return await repository.Add(escort);
	}

	public async Task<Escort> Update(Escort escort)
	{
		if (!await repository.ExistsByIdAndPeopleId(escort.Id, escort.PeopleId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

		return await repository.Update(escort);
	}

	public async Task<Escort> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

		return await repository.RemoveById(id);
	}
}
