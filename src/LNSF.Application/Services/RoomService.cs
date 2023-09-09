using LNSF.Domain;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Application;

public class RoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly RoomValidator _roomValidator;
    private readonly PaginationValidator _paginationValidator;

    public RoomService(IRoomRepository roomRepository,
        RoomValidator roomValidator,
        PaginationValidator paginationValidator)
    {
        _roomRepository = roomRepository;
        _roomValidator = roomValidator;
        _paginationValidator = paginationValidator;
    }

    public async Task<ResultDTO<List<Room>>> Get(Pagination pagination)
    {
        var validationResult = _paginationValidator.Validate(pagination);

        return validationResult.IsValid ?
            await _roomRepository.Get(pagination) :
            new ResultDTO<List<Room>>(validationResult.ToString());
    }

    public async Task<ResultDTO<Room>> Get(int id) => 
        await _roomRepository.Get(id);

    public async Task<ResultDTO<Room>> Post(Room room)
    {
        var validationResult = _roomValidator.Validate(room);

        if (!validationResult.IsValid) return new ResultDTO<Room>(validationResult.ToString());

        room.Id = null; // Null Id to insert into in data base

        return await _roomRepository.Post(room);
    }

    public async Task<ResultDTO<Room>> Put(Room room)
    {
        var validationResult = _roomValidator.Validate(room);

        return validationResult.IsValid ?
            await _roomRepository.Put(room) :
            new ResultDTO<Room>(validationResult.ToString());
    }
}
