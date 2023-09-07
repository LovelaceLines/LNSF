using LNSF.Domain;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class RoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly RoomValidator _roomValidator;
    private readonly RoomAddValidator _roomAddValidator;

    public RoomService(IRoomRepository roomRepository,
        RoomValidator roomValidator,
        RoomAddValidator roomAddValidator)
    {
        _roomRepository = roomRepository;
        _roomValidator = roomValidator;
        _roomAddValidator = roomAddValidator;
    }

    public async Task<List<Room>> Get()
    {
        return await _roomRepository.Get();
    }

    public async Task<Room> Get(int id)
    {
        return await _roomRepository.Get(id);
    }

    public async Task<Room> Add(IRoomAdd room)
    {
        var validationResult = _roomAddValidator.Validate(room);

        if (!validationResult.IsValid)
        {
            return new Room();
        }

        Room _room = new()
        {
            Bathroom = room.Bathroom,
            Available = true,
            Beds = room.Beds,
            Number = room.Number,
            Occupation = room.Occupation,
            Storey = room.Storey,
        };

        return await _roomRepository.Add(_room);
    }

    public async Task<Room> Update(Room room)
    {
        var validationResult = _roomValidator.Validate(room);

        if (!validationResult.IsValid)
        {
            return new Room();
        }

        return await _roomRepository.Update(room);
    }

    public async Task<bool> Available(int id)
    {
        return await _roomRepository.Available(id);
    }

    public async Task<int> GetOccupation(int id)
    {
        return await _roomRepository.GetOccupation(id);
    }
}
