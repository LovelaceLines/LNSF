using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRoomRepository : BaseRepository<PeopleRoom>, IPeopleRoomRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<PeopleRoom> _peoplesRooms;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Room> _rooms;
    private readonly IQueryable<Hosting> _hostings;

    public PeopleRoomRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoplesRooms = _context.PeoplesRooms.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
        _rooms = _context.Rooms.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
    }

    public async Task<List<PeopleRoom>> Query(PeopleRoomFilter filter)
    {
        var query = _peoplesRooms;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobal(query, filter.GlobalFilter!, _peoples, _rooms);

        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value, _peoples);
        if (filter.RoomId.HasValue) query = QueryRoomId(query, filter.RoomId.Value, _rooms);
        if (filter.HostingId.HasValue) query = QueryHostingId(query, filter.HostingId.Value, _hostings);
        if (filter.CheckIn.HasValue) query = QueryCheckIn(query, filter.CheckIn.Value, _hostings);
        if (filter.CheckOut.HasValue) query = QueryCheckOut(query, filter.CheckOut.Value, _hostings);
        if (filter.Available.HasValue) query = query.Where(pr => pr.Room!.Available == filter.Available);

        if (filter.HasVacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            query = QueryHasVacancy(query, filter.HasVacancy.Value, filter.CheckIn.Value, filter.CheckOut.Value, _peoplesRooms);
        if (filter.Vacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            query = QueryVacancy(query, filter.Vacancy.Value, filter.CheckIn.Value, filter.CheckOut.Value, _peoplesRooms);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(pr => pr.RoomId);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(pr => pr.RoomId);

        var peoplesRooms = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPeople ?? false, filter.GetRoom ?? false, filter.GetHosting ?? false))
            .ToListAsync();

        return peoplesRooms;
    }

    private IQueryable<PeopleRoom> QueryGlobal(IQueryable<PeopleRoom> query, string globalFilter, IQueryable<People> peoples, IQueryable<Room> rooms) =>
        query.Where(pr => PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(p => p.Id == pr.PeopleId) ||
            RoomRepository.QueryGlobal(rooms, globalFilter).Any(r => r.Id == pr.RoomId));

    private static Expression<Func<PeopleRoom, PeopleRoom>> Build(bool getPeople, bool getRoom, bool getHosting) =>
        pr => new PeopleRoom
        {
            PeopleId = pr.PeopleId,
            RoomId = pr.RoomId,
            HostingId = pr.HostingId,
            People = getPeople == true ? pr.People : null,
            Room = getRoom == true ? pr.Room : null,
            Hosting = getHosting == true ? pr.Hosting : null
        };

    public static IQueryable<PeopleRoom> QueryPeopleId(IQueryable<PeopleRoom> query, int id, IQueryable<People> peoples) =>
        query.Where(pr => PeopleRepository.QueryPeopleId(peoples, id).Any(p => p.Id == pr.PeopleId));

    public static IQueryable<PeopleRoom> QueryRoomId(IQueryable<PeopleRoom> query, int id, IQueryable<Room> rooms) =>
        query.Where(pr => RoomRepository.QueryRoomId(rooms, id).Any(r => r.Id == pr.RoomId));

    public static IQueryable<PeopleRoom> QueryHostingId(IQueryable<PeopleRoom> query, int id, IQueryable<Hosting> hostings) =>
        query.Where(pr => HostingRepository.QueryHostingId(hostings, id).Any(h => h.Id == pr.HostingId));

    public static IQueryable<PeopleRoom> QueryCheckIn(IQueryable<PeopleRoom> query, DateTime checkIn, IQueryable<Hosting> hostings) =>
        query.Where(pr => HostingRepository.QueryCheckIn(hostings, checkIn).Any(h => h.Id == pr.HostingId));

    public static IQueryable<PeopleRoom> QueryCheckOut(IQueryable<PeopleRoom> query, DateTime checkOut, IQueryable<Hosting> hostings) =>
        query.Where(pr => HostingRepository.QueryCheckOut(hostings, checkOut).Any(h => h.Id == pr.HostingId));

    public static IQueryable<PeopleRoom> QueryHasVacancy(IQueryable<PeopleRoom> query, bool hasVacancy, DateTime checkIn, DateTime checkOut, IQueryable<PeopleRoom> peoplesRooms) =>
        hasVacancy ?
            query.Where(pr => pr.Room!.Beds >
                peoplesRooms.Count(pr2 => pr2.RoomId == pr.RoomId &&
                    checkIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= checkOut)) :
            query.Where(pr => pr.Room!.Beds <=
                peoplesRooms.Count(pr2 => pr2.RoomId == pr.RoomId &&
                    checkIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= checkOut));

    public static IQueryable<PeopleRoom> QueryVacancy(IQueryable<PeopleRoom> query, int vacancy, DateTime checkIn, DateTime checkOut, IQueryable<PeopleRoom> peoplesRooms) =>
        query.Where(pr => pr.Room!.Beds - peoplesRooms.Count(pr2 => pr2.RoomId == pr.RoomId &&
            checkIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= checkOut) >= vacancy);

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
