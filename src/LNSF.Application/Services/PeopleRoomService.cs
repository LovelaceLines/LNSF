using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class PeopleRoomService : IPeopleRoomService
{
    private readonly IPeopleRoomRepository _peopleRoomRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IHostingRepository _hostingRepository;

    public PeopleRoomService(IPeopleRoomRepository peopleRoomRepository,
        IRoomRepository roomRepository,
        IPeopleRepository peopleRepository,
        IHostingRepository hostingRepository)
    {
        _peopleRoomRepository = peopleRoomRepository;
        _roomRepository = roomRepository;
        _peopleRepository = peopleRepository;
        _hostingRepository = hostingRepository;
    }

    public async Task<List<PeopleRoom>> Query(PeopleRoomFilter filter) => 
        await _peopleRoomRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _peopleRoomRepository.GetCount();

    public async Task<PeopleRoom> Create(PeopleRoom peopleRoom)
    {
        if (await _peopleRoomRepository.ExistsHosting(peopleRoom)) throw new AppException("Pessoa já está hospedada!", HttpStatusCode.Conflict);
        if (await _peopleRoomRepository.ExistsByPeopleRoom(peopleRoom)) throw new AppException("Registro já existe!", HttpStatusCode.Conflict);
        if (!await _hostingRepository.ExistsByIdAndPeopleId(peopleRoom.HostingId, peopleRoom.PeopleId)) throw new AppException("Pessoa não encontrada na hospedagem!", HttpStatusCode.NotFound);

        if (!await _peopleRepository.ExistsById(peopleRoom.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
        if (!await _roomRepository.ExistsById(peopleRoom.RoomId)) throw new AppException("Quarto não encontrado!", HttpStatusCode.NotFound);
        if (!await _hostingRepository.ExistsById(peopleRoom.HostingId)) throw new AppException("Hospedagem não encontrada!", HttpStatusCode.NotFound);

        peopleRoom.Room = await _roomRepository.GetById(peopleRoom.RoomId);
        peopleRoom.Hosting = await _hostingRepository.GetById(peopleRoom.HostingId);
        peopleRoom.People = await _peopleRepository.GetById(peopleRoom.PeopleId);

        if (!peopleRoom.Room.Available) throw new AppException("Quarto indisponível!", HttpStatusCode.Conflict);
        if (!await _peopleRoomRepository.HaveVacancy(peopleRoom)) throw new AppException("Quarto sem vagas!", HttpStatusCode.Conflict);

        peopleRoom.Room = null;
        peopleRoom.Hosting = null;
        peopleRoom.People = null;
        
        return await _peopleRoomRepository.Add(peopleRoom);
    }

    public async Task<PeopleRoom> Delete(PeopleRoom peopleRoom)
    {
        if (!await _peopleRoomRepository.ExistsById(peopleRoom.RoomId, peopleRoom.PeopleId, peopleRoom.HostingId)) throw new AppException("Registro não encontrado!", HttpStatusCode.NotFound);

        return await _peopleRoomRepository.Remove(peopleRoom);
    }
}
