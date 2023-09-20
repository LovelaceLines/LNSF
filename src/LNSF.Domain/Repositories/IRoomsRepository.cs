using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IRoomsRepository : IBaseRepository<Room>
{
    public Task<List<Room>> Get(RoomFilters filters);
}
