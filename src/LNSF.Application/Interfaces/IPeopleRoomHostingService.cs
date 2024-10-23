using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IPeopleRoomHostingService : IBaseService<PeopleRoomHosting>
{
	Task<PeopleRoomHosting> Delete(PeopleRoomHosting peopleRoomHosting);
}
