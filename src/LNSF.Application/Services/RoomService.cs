using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using System.Net;

namespace LNSF.Application.Services;

public class RoomService
{
    private readonly IRoomsRepository _roomRepository;
    private readonly RoomValidator _roomValidator;

    public RoomService(IRoomsRepository roomRepository,
        RoomValidator roomValidator)
    {
        _roomRepository = roomRepository;
        _roomValidator = roomValidator;
    }

    public async Task<List<Room>> Query(RoomFilter filter) => 
        await _roomRepository.Query(filter);

    public async Task<Room> Get(int id) => 
        await _roomRepository.Get(id);

    public async Task<int> GetCount() =>
        await _roomRepository.GetCount();

    public async Task<Room> Create(Room room)
    {
        var validationResult = _roomValidator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _roomRepository.Add(room);
    }

    public async Task<Room> Update(Room room)
    {
        var validationResult = _roomValidator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _roomRepository.Update(room);
    }
}
