using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<List<Room>> Query(RoomFilter filter);
    Task<bool> ExistsByNumber(string number);
}
