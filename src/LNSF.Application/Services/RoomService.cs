using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;

namespace LNSF.Application.Services;

public class RoomService
{
    private readonly IRoomsRepository _roomRepository;
    private readonly RoomValidator _roomValidator;
    private readonly RoomFilterValidator _roomFilterValidator;

    public RoomService(IRoomsRepository roomRepository,
        RoomValidator roomValidator,
        RoomFilterValidator roomFilterValidator)
    {
        _roomRepository = roomRepository;
        _roomValidator = roomValidator;
        _roomFilterValidator = roomFilterValidator;
    }

    public async Task<List<Room>> Query(RoomFilter filter)
    {
        var validationResult = _roomFilterValidator.Validate(filter);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _roomRepository.Query(filter);
    }

    public async Task<Room> Get(int id) => 
        await _roomRepository.Get(id);

    public async Task<int> GetQuantity() =>
        await _roomRepository.GetQuantity();

    public async Task<Room> Create(Room room)
    {
        var validationResult = _roomValidator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _roomRepository.Post(room);
    }

    public async Task<Room> Update(Room room)
    {
        var validationResult = _roomValidator.Validate(room);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _roomRepository.Put(room);
    }
}
