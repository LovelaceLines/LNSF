using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IRoomService
{
    Task<List<Room>> Query(RoomFilter filter);
    Task<int> GetCount();
    Task<Room> Create(Room room);
    Task<Room> Update(Room room);
}
