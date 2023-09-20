using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;

namespace LNSF.Application.Services;

public class RoomService
{
    private readonly IRoomsRepository _roomRepository;
    private readonly RoomValidator _roomValidator;
    private readonly RoomFiltersValidator _roomFiltersValidator;

    public RoomService(IRoomsRepository roomRepository,
        RoomValidator roomValidator,
        RoomFiltersValidator roomFiltersValidator)
    {
        _roomRepository = roomRepository;
        _roomValidator = roomValidator;
        _roomFiltersValidator = roomFiltersValidator;
    }

    public async Task<List<Room>> Get(RoomFilters filters)
    {
        var validationResult = _roomFiltersValidator.Validate(filters);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _roomRepository.Get(filters);
    }

    public async Task<Room> Get(int id) => 
        await _roomRepository.Get(id);

    public async Task<int> GetQuantity() =>
        await _roomRepository.GetQuantity();

    public async Task<Room> Post(Room room)
    {
        var validationResult = _roomValidator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _roomRepository.Post(room);
    }

    public async Task<Room> Put(Room room)
    {
        var validationResult = _roomValidator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _roomRepository.Put(room);
    }
}
