using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PeopleRoomHostingService : IPeopleRoomHostingService
{
    private readonly IPeopleRoomHostingRepository _peopleRoomHostingRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IHostingRepository _hostingRepository;

    public PeopleRoomHostingService(IPeopleRoomHostingRepository peopleRoomHostingRepository,
        IRoomRepository roomRepository,
        IPeopleRepository peopleRepository,
        IHostingRepository hostingRepository)
    {
        _peopleRoomHostingRepository = peopleRoomHostingRepository;
        _roomRepository = roomRepository;
        _peopleRepository = peopleRepository;
        _hostingRepository = hostingRepository;
    }

    public async Task<List<PeopleRoomHosting>> Query(PeopleRoomHostingFilter filter) =>
        await _peopleRoomHostingRepository.Query(filter);

    public async Task<int> GetCount() =>
        await _peopleRoomHostingRepository.GetCount();

    public async Task<PeopleRoomHosting> Create(PeopleRoomHosting peopleRoomHosting)
    {
        if (await _peopleRoomHostingRepository.ExistsHosting(peopleRoomHosting)) throw new AppException("Pessoa já está hospedada!", HttpStatusCode.Conflict);
        if (await _peopleRoomHostingRepository.ExistsByPeopleRoomHosting(peopleRoomHosting)) throw new AppException("Registro já existe!", HttpStatusCode.Conflict);
        if (!await _hostingRepository.ExistsByIdAndPeopleId(peopleRoomHosting.HostingId, peopleRoomHosting.PeopleId)) throw new AppException("Pessoa não encontrada na hospedagem!", HttpStatusCode.NotFound);

        if (!await _peopleRepository.ExistsById(peopleRoomHosting.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
        if (!await _roomRepository.ExistsById(peopleRoomHosting.RoomId)) throw new AppException("Quarto não encontrado!", HttpStatusCode.NotFound);
        if (!await _hostingRepository.ExistsById(peopleRoomHosting.HostingId)) throw new AppException("Hospedagem não encontrada!", HttpStatusCode.NotFound);

        peopleRoomHosting.Room = await _roomRepository.GetById(peopleRoomHosting.RoomId);
        peopleRoomHosting.Hosting = await _hostingRepository.GetById(peopleRoomHosting.HostingId);
        peopleRoomHosting.People = await _peopleRepository.GetById(peopleRoomHosting.PeopleId);

        if (!peopleRoomHosting.Room.Available) throw new AppException("Quarto indisponível!", HttpStatusCode.Conflict);
        if (!await _peopleRoomHostingRepository.HaveVacancy(peopleRoomHosting)) throw new AppException("Quarto sem vagas!", HttpStatusCode.Conflict);

        peopleRoomHosting.Room = null;
        peopleRoomHosting.Hosting = null;
        peopleRoomHosting.People = null;

        return await _peopleRoomHostingRepository.Add(peopleRoomHosting);
    }

    public async Task<PeopleRoomHosting> Delete(PeopleRoomHosting peopleRoomHosting)
    {
        if (!await _peopleRoomHostingRepository.ExistsById(peopleRoomHosting.RoomId, peopleRoomHosting.PeopleId, peopleRoomHosting.HostingId)) throw new AppException("Registro não encontrado!", HttpStatusCode.NotFound);

        return await _peopleRoomHostingRepository.Remove(peopleRoomHosting);
    }
}
