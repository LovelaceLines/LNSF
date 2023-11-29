using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPeopleRoomService
{
    Task<List<PeopleRoom>> Query(PeopleRoomFilter filter);
    Task<int> GetCount();
    Task<PeopleRoom> Create(PeopleRoom peopleRoom);
    Task<PeopleRoom> Delete(int id);
}
