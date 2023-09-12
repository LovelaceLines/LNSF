using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IRoomsRepository
{
    public Task<ResultDTO<List<Room>>> Get(RoomFilters filters);
    public Task<ResultDTO<Room>> Get(int id);
    public Task<ResultDTO<int>> GetQuantity();
    public Task<ResultDTO<Room>> Post(Room room);
    public Task<ResultDTO<Room>> Put(Room room);
}
