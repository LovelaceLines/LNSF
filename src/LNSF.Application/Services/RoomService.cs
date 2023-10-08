using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using System.Net;
using LNSF.Application.Interfaces;

namespace LNSF.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomsRepository _roomRepository;
    private readonly RoomValidator _validator;

    public RoomService(IRoomsRepository roomRepository,
        RoomValidator validator)
    {
        _roomRepository = roomRepository;
        _validator = validator;
    }

    public async Task<List<Room>> Query(RoomFilter filter) => 
        await _roomRepository.Query(filter);

    public async Task<Room> Get(int id) => 
        await _roomRepository.Get(id);

    public async Task<int> GetCount() =>
        await _roomRepository.GetCount();

    public async Task<Room> Create(Room room)
    {
        var validationResult = _validator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _roomRepository.Add(room);
    }

    public async Task<Room> Update(Room room)
    {
        var validationResult = _validator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _roomRepository.Update(room);
    }
}
