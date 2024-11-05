using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PeopleRoomHostingService(IPeopleRoomHostingRepository repository,
	IRoomRepository roomRepository,
	IPeopleRepository peopleRepository,
	IHostingRepository hostingRepository) : IPeopleRoomHostingService
{
	public async Task<PeopleRoomHosting> Create(PeopleRoomHosting peopleRoomHosting)
	{
		if (await repository.ExistsHosting(peopleRoomHosting)) throw new AppException("Pessoa já está hospedada!", HttpStatusCode.Conflict);
		if (await repository.ExistsByPeopleRoomHosting(peopleRoomHosting)) throw new AppException("Registro já existe!", HttpStatusCode.Conflict);
		if (!await hostingRepository.ExistsByIdAndPeopleId(peopleRoomHosting.HostingId, peopleRoomHosting.PeopleId)) throw new AppException("Pessoa não encontrada na hospedagem!", HttpStatusCode.NotFound);

		if (!await peopleRepository.ExistsById(peopleRoomHosting.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
		if (!await roomRepository.ExistsById(peopleRoomHosting.RoomId)) throw new AppException("Quarto não encontrado!", HttpStatusCode.NotFound);
		if (!await hostingRepository.ExistsById(peopleRoomHosting.HostingId)) throw new AppException("Hospedagem não encontrada!", HttpStatusCode.NotFound);

		peopleRoomHosting.Room = await roomRepository.GetById(peopleRoomHosting.RoomId);
		peopleRoomHosting.Hosting = await hostingRepository.GetById(peopleRoomHosting.HostingId);
		peopleRoomHosting.People = await peopleRepository.GetById(peopleRoomHosting.PeopleId);

		if (!peopleRoomHosting.Room.Available) throw new AppException("Quarto indisponível!", HttpStatusCode.Conflict);
		if (!await repository.HaveVacancy(peopleRoomHosting)) throw new AppException("Quarto sem vagas!", HttpStatusCode.Conflict);

		peopleRoomHosting.Room = null;
		peopleRoomHosting.Hosting = null;
		peopleRoomHosting.People = null;

		return await repository.Add(peopleRoomHosting);
	}

	public async Task<PeopleRoomHosting> Delete(PeopleRoomHosting peopleRoomHosting)
	{
		if (!await repository.ExistsByPeopleIdRoomIdHostingId(peopleRoomHosting.PeopleId, peopleRoomHosting.RoomId, peopleRoomHosting.HostingId)) throw new AppException("Registro não encontrado!", HttpStatusCode.NotFound);
		peopleRoomHosting = await repository.GetByPeopleIdRoomIdHostingId(peopleRoomHosting.PeopleId, peopleRoomHosting.RoomId, peopleRoomHosting.HostingId);

		return await repository.RemoveById(peopleRoomHosting.Id);
	}
}
