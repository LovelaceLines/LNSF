using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IRoomsRepository : IBaseRepository<Room>
{
    public Task<List<Room>> Query(RoomFilter filter);
}
