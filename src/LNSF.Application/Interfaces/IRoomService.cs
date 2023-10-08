using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IRoomService
{
    public Task<List<Room>> Query(RoomFilter filter);
    public Task<int> GetCount();
    public Task<Room> Create(Room room);
    public Task<Room> Update(Room room);
}
