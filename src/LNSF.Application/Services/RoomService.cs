using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly RoomValidator _validator;

    public RoomService(IRoomRepository roomRepository,
        RoomValidator validator)
    {
        _roomRepository = roomRepository;
        _validator = validator;
    }

    public async Task<List<Room>> Query(RoomFilter filter) => 
        await _roomRepository.Query(filter);

    public async Task<int> GetCount() =>
        await _roomRepository.GetCount();

    public async Task<Room> Create(Room room)
    {
        var validationResult = _validator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (await _roomRepository.ExistsByNumber(room.Number)) throw new AppException("Número do quarto já existe!", HttpStatusCode.Conflict);
        
        return await _roomRepository.Add(room);
    }

    public async Task<Room> Update(Room newRoom)
    {
        var validationResult = _validator.Validate(newRoom);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        var oldRoom = await _roomRepository.GetById(newRoom.Id);
        if (oldRoom.Number != newRoom.Number && await _roomRepository.ExistsByNumber(newRoom.Number)) throw new AppException("Número do quarto já existe!", HttpStatusCode.Conflict);

        return await _roomRepository.Update(newRoom);
    }
}
