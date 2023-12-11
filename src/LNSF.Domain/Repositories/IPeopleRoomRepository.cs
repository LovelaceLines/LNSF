using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPeopleRoomRepository : IBaseRepository<PeopleRoom>
{
    Task<List<PeopleRoom>> Query(PeopleRoomFilter filter);
    Task<bool> ExistsHosting(PeopleRoom peopleRoom);
    Task<bool> ExistsByPeopleRoom(PeopleRoom peopleRoom);

    /// <summary>
    /// Checks if a given PeopleRoom has vacancy in the corresponding room.
    /// <param name="peopleRoom">Require that Entities Hosting, People and Room exists</param>
    /// </summary>
    /// <returns>True if there is vacancy, false otherwise.</returns>
    Task<bool> HaveVacancy(PeopleRoom peopleRoom);
    Task<int> GetOccupation(PeopleRoom peopleRoom);
}
