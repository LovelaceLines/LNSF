using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRoomRepository : BaseRepository<PeopleRoom>, IPeopleRoomRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<PeopleRoom> _peoplesRooms;
    private readonly IQueryable<Room> _rooms;

    public PeopleRoomRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoplesRooms = _context.PeoplesRooms.AsNoTracking();
        _rooms = _context.Rooms.AsNoTracking();
    }

    public async Task<List<PeopleRoom>> Query(PeopleRoomFilter filter)
    {
        var query = _peoplesRooms;

        if (filter.CheckIn.HasValue) query = query.Where(pr => filter.CheckIn <= pr.Hosting!.CheckIn);
        if (filter.CheckOut.HasValue) query = query.Where(pr => pr.Hosting!.CheckOut <= filter.CheckOut);
        if (filter.Available.HasValue) query = query.Where(pr => pr.Room!.Available == filter.Available);
        
        if (filter.HaveVacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue) 
            query = query.Where(pr => pr.Room!.Beds > 
                _peoplesRooms.Count(pr2 => pr2.RoomId == pr.RoomId && 
                    filter.CheckIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= filter.CheckOut));
        else if (!filter.HaveVacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue) 
            query = query.Where(pr => pr.Room!.Beds <= 
                _peoplesRooms.Count(pr2 => pr2.RoomId == pr.RoomId && 
                    filter.CheckIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= filter.CheckOut));
        
        if (filter.Vacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            query = query.Where(pr => pr.Room!.Beds - 
                _peoplesRooms.Count(pr2 => pr2.RoomId == pr.RoomId && 
                    filter.CheckIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= filter.CheckOut) == filter.Vacancy);
        
        if (filter.PeopleId.HasValue) query = query.Where(pr => pr.PeopleId == filter.PeopleId);
        if (filter.RoomId.HasValue) query = query.Where(pr => pr.RoomId == filter.RoomId);
        if (filter.HostingId.HasValue) query = query.Where(pr => pr.HostingId == filter.HostingId);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(pr => pr.RoomId);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(pr => pr.RoomId);

        var peoplesRooms = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return peoplesRooms;
    }

    public async Task<bool> ExistsHosting(PeopleRoom peopleRoom) => 
        await _peoplesRooms.AnyAsync(pr => pr.PeopleId == peopleRoom.PeopleId && 
            pr.HostingId == peopleRoom.HostingId);

    public async Task<bool> ExistsByPeopleRoom(PeopleRoom peopleRoom) => 
        await _peoplesRooms.AnyAsync(pr => pr.PeopleId == peopleRoom.PeopleId && 
            pr.RoomId == peopleRoom.RoomId && pr.HostingId == peopleRoom.HostingId);
    
    public async Task<bool> HaveVacancy(PeopleRoom peopleRoom)
    {
        var room = await _rooms.FirstOrDefaultAsync(r => r.Id == peopleRoom.RoomId);
        var beds = room!.Beds;
        var occupation = await GetOccupation(peopleRoom);

        return beds > occupation;
    }

    public async Task<int> GetOccupation(PeopleRoom peopleRoom) => 
        await _peoplesRooms.CountAsync(pr => pr.RoomId == peopleRoom.RoomId &&
            pr.Hosting!.CheckIn <= peopleRoom.Hosting!.CheckIn && peopleRoom.Hosting.CheckIn <= pr.Hosting.CheckOut &&
            pr.Hosting.CheckIn <= peopleRoom.Hosting.CheckOut && peopleRoom.Hosting.CheckOut <= pr.Hosting.CheckOut);
}
