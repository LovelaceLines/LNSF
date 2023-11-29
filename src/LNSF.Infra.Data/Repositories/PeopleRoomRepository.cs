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
        var query = _context.PeoplesRooms.AsNoTracking();

        if (filter.PeopleId.HasValue) query = query.Where(pr => pr.PeopleId == filter.PeopleId);
        if (filter.RoomId.HasValue) query = query.Where(pr => pr.RoomId == filter.RoomId);
        if (filter.HostingId.HasValue) query = query.Where(pr => pr.HostingId == filter.HostingId);
        if (filter.CheckIn.HasValue) query = query.Where(pr => pr.Hosting!.CheckIn == filter.CheckIn);
        if (filter.CheckOut.HasValue) query = query.Where(pr => pr.Hosting!.CheckOut == filter.CheckOut);
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(pr => pr.RoomId);
        else query = query.OrderByDescending(pr => pr.RoomId);
        
        if (filter.Occupation.HasValue && filter.Id.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue)
        {
            var occupation = await _peoplesRooms.CountAsync(pr => pr.RoomId == filter.Id &&
                pr.Hosting!.CheckIn <= filter.CheckIn && filter.CheckIn <= pr.Hosting.CheckOut &&
                pr.Hosting.CheckIn <= filter.CheckOut && filter.CheckOut <= pr.Hosting.CheckOut);
            
            query = query.Where(pr => occupation == filter.Occupation);
        }

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
