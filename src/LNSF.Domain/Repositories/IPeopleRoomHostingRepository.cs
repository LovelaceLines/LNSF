using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPeopleRoomHostingRepository : IBaseRepository<PeopleRoomHosting>
{
    Task<List<PeopleRoomHosting>> Query(PeopleRoomHostingFilter filter);
    Task<bool> ExistsHosting(PeopleRoomHosting peopleRoomHosting);
    Task<bool> ExistsByPeopleRoomHosting(PeopleRoomHosting peopleRoomHosting);

    /// <summary>
    /// Checks if a given PeopleRoomHosting has vacancy in the corresponding room.
    /// <param name="peopleRoomHosting">Require that Entities Hosting, People and Room exists</param>
    /// </summary>
    /// <returns>True if there is vacancy, false otherwise.</returns>
    Task<bool> HaveVacancy(PeopleRoomHosting peopleRoomHosting);
    Task<int> GetOccupation(PeopleRoomHosting peopleRoomHosting);
}
