using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPeopleRoomRepository : IBaseRepository<PeopleRoom>
{
    Task<List<PeopleRoom>> Query(PeopleRoomFilter filter);
    Task<bool> ExistsHosting(PeopleRoom peopleRoom);
    Task<bool> ExistsByPeopleRoom(PeopleRoom peopleRoom);
    Task<bool> HaveVacancy(PeopleRoom peopleRoom);
    Task<int> GetOccupation(PeopleRoom peopleRoom);
}
