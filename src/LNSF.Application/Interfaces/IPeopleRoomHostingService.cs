using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPeopleRoomHostingService
{
    Task<List<PeopleRoomHosting>> Query(PeopleRoomHostingFilter filter);
    Task<int> GetCount();
    Task<PeopleRoomHosting> Create(PeopleRoomHosting peopleRoomHosting);
    Task<PeopleRoomHosting> Delete(PeopleRoomHosting peopleRoomHosting);
}
