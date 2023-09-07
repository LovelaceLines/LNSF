using LNSF.Domain.Entities;

namespace LNSF.Domain;

public interface IRoomRepository
{
    public Task<List<Room>> Get();
    public Task<Room> Get(int id);
    public Task<Room> Add(Room room);
    public Task<Room> Update(Room room);

    public Task<bool> Available(int id);
    public Task<int> GetOccupation(int roomId);
}
