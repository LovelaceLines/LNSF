using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Application.Services;

public class RoomService
{
    private readonly IRoomsRepository _roomRepository;
    private readonly RoomValidator _roomValidator;
    private readonly PaginationValidator _paginationValidator;

    public RoomService(IRoomsRepository roomRepository,
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

    public async Task<ResultDTO<int>> GetQuantity() =>
        await _roomRepository.GetQuantity();

    public async Task<ResultDTO<Room>> Post(Room room)
    {
        room.Id = 0;

        var validationResult = _roomValidator.Validate(room);

        return validationResult.IsValid ? 
            await _roomRepository.Post(room) :
            new ResultDTO<Room>(validationResult.ToString());
    }

    public async Task<ResultDTO<Room>> Put(Room room)
    {
        if (room.Id == 0) return new ResultDTO<Room>("Quarto não encontrado");
        
        var validationResult = _roomValidator.Validate(room);

        return validationResult.IsValid ?
            await _roomRepository.Put(room) :
            new ResultDTO<Room>(validationResult.ToString());
    }
}
