using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRoomHostingRepository : BaseRepository<PeopleRoomHosting>, IPeopleRoomHostingRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<PeopleRoomHosting> _peoplesRoomsHostings;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;
    private readonly IQueryable<Room> _rooms;
    private readonly IQueryable<Hosting> _hostings;

    public PeopleRoomHostingRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoplesRoomsHostings = _context.PeoplesRoomsHostings.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
        _patients = _context.Patients.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        _rooms = _context.Rooms.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
    }

    public async Task<List<PeopleRoomHosting>> Query(PeopleRoomHostingFilter filter)
    {
        var query = _peoplesRoomsHostings;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobal(query, filter.GlobalFilter!, _peoples, _rooms);

        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value, _peoples);
        if (filter.RoomId.HasValue) query = QueryRoomId(query, filter.RoomId.Value, _rooms);
        if (filter.HostingId.HasValue) query = QueryHostingId(query, filter.HostingId.Value, _hostings);
        if (filter.CheckIn.HasValue) query = QueryCheckIn(query, filter.CheckIn.Value, _hostings);
        if (filter.CheckOut.HasValue) query = QueryCheckOut(query, filter.CheckOut.Value, _hostings);
        if (filter.Available.HasValue) query = query.Where(prh => prh.Room!.Available == filter.Available);

        if (filter.Active.HasValue) query = QueryActive(query, filter.Active.Value, _peoples, _patients, _escorts, _hostingsEscorts, _hostings);
        if (filter.HasVacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            query = QueryHasVacancy(query, filter.HasVacancy.Value, filter.CheckIn.Value, filter.CheckOut.Value, _peoplesRoomsHostings);
        if (filter.Vacancy.HasValue && filter.CheckIn.HasValue && filter.CheckOut.HasValue)
            query = QueryVacancy(query, filter.Vacancy.Value, filter.CheckIn.Value, filter.CheckOut.Value, _peoplesRoomsHostings);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(prh => prh.HostingId);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(prh => prh.HostingId);

        var peoplesRooms = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPeople ?? false, filter.GetRoom ?? false, filter.GetHosting ?? false))
            .ToListAsync();

        return peoplesRooms;
    }

    private static IQueryable<PeopleRoomHosting> QueryActive(IQueryable<PeopleRoomHosting> query, bool active, IQueryable<People> peoples, IQueryable<Patient> patients, IQueryable<Escort> escorts, IQueryable<HostingEscort> hostingsEscorts, IQueryable<Hosting> hostings) =>
        query.Where(prh => PeopleRepository.QueryActive(peoples, active, patients, escorts, hostings, hostingsEscorts).Any(p => p.Id == prh.PeopleId));

    private static IQueryable<PeopleRoomHosting> QueryGlobal(IQueryable<PeopleRoomHosting> query, string globalFilter, IQueryable<People> peoples, IQueryable<Room> rooms) =>
        query.Where(prh => PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(p => p.Id == prh.PeopleId) ||
            RoomRepository.QueryGlobal(rooms, globalFilter).Any(r => r.Id == prh.RoomId));

    private static Expression<Func<PeopleRoomHosting, PeopleRoomHosting>> Build(bool getPeople, bool getRoom, bool getHosting) =>
        prh => new PeopleRoomHosting
        {
            PeopleId = prh.PeopleId,
            RoomId = prh.RoomId,
            HostingId = prh.HostingId,
            People = getPeople == true ? prh.People : null,
            Room = getRoom == true ? prh.Room : null,
            Hosting = getHosting == true ? prh.Hosting : null
        };

    public static IQueryable<PeopleRoomHosting> QueryPeopleId(IQueryable<PeopleRoomHosting> query, int id, IQueryable<People> peoples) =>
        query.Where(prh => PeopleRepository.QueryPeopleId(peoples, id).Any(p => p.Id == prh.PeopleId));

    public static IQueryable<PeopleRoomHosting> QueryRoomId(IQueryable<PeopleRoomHosting> query, int id, IQueryable<Room> rooms) =>
        query.Where(prh => RoomRepository.QueryRoomId(rooms, id).Any(r => r.Id == prh.RoomId));

    public static IQueryable<PeopleRoomHosting> QueryHostingId(IQueryable<PeopleRoomHosting> query, int id, IQueryable<Hosting> hostings) =>
        query.Where(prh => HostingRepository.QueryHostingId(hostings, id).Any(h => h.Id == prh.HostingId));

    public static IQueryable<PeopleRoomHosting> QueryCheckIn(IQueryable<PeopleRoomHosting> query, DateTime checkIn, IQueryable<Hosting> hostings) =>
        query.Where(prh => HostingRepository.QueryCheckIn(hostings, checkIn).Any(h => h.Id == prh.HostingId));

    public static IQueryable<PeopleRoomHosting> QueryCheckOut(IQueryable<PeopleRoomHosting> query, DateTime checkOut, IQueryable<Hosting> hostings) =>
        query.Where(prh => HostingRepository.QueryCheckOut(hostings, checkOut).Any(h => h.Id == prh.HostingId));

    public static IQueryable<PeopleRoomHosting> QueryHasVacancy(IQueryable<PeopleRoomHosting> query, bool hasVacancy, DateTime checkIn, DateTime checkOut, IQueryable<PeopleRoomHosting> peoplesRooms) =>
        hasVacancy ?
            query.Where(prh => prh.Room!.Beds >
                peoplesRooms.Count(pr2 => pr2.RoomId == prh.RoomId &&
                    checkIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= checkOut)) :
            query.Where(prh => prh.Room!.Beds <=
                peoplesRooms.Count(pr2 => pr2.RoomId == prh.RoomId &&
                    checkIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= checkOut));

    public static IQueryable<PeopleRoomHosting> QueryVacancy(IQueryable<PeopleRoomHosting> query, int vacancy, DateTime checkIn, DateTime checkOut, IQueryable<PeopleRoomHosting> peoplesRooms) =>
        query.Where(prh => prh.Room!.Beds - peoplesRooms.Count(pr2 => pr2.RoomId == prh.RoomId &&
            checkIn <= pr2.Hosting!.CheckIn && pr2.Hosting.CheckOut <= checkOut) >= vacancy);

    public async Task<bool> ExistsHosting(PeopleRoomHosting peopleRoomHosting) =>
        await _peoplesRoomsHostings.AnyAsync(prh => prh.PeopleId == peopleRoomHosting.PeopleId &&
            prh.HostingId == peopleRoomHosting.HostingId);

    public async Task<bool> ExistsByPeopleRoomHosting(PeopleRoomHosting peopleRoomHosting) =>
        await _peoplesRoomsHostings.AnyAsync(prh => prh.PeopleId == peopleRoomHosting.PeopleId &&
            prh.RoomId == peopleRoomHosting.RoomId && prh.HostingId == peopleRoomHosting.HostingId);

    public async Task<bool> HaveVacancy(PeopleRoomHosting peopleRoomHosting)
    {
        var room = await _rooms.FirstOrDefaultAsync(r => r.Id == peopleRoomHosting.RoomId);
        var beds = room!.Beds;
        var occupation = await GetOccupation(peopleRoomHosting);

        return beds > occupation;
    }

    public async Task<int> GetOccupation(PeopleRoomHosting peopleRoomHosting) =>
        await _peoplesRoomsHostings.CountAsync(prh => prh.RoomId == peopleRoomHosting.RoomId &&
            prh.Hosting!.CheckIn <= peopleRoomHosting.Hosting!.CheckIn && peopleRoomHosting.Hosting.CheckIn <= prh.Hosting.CheckOut &&
            prh.Hosting.CheckIn <= peopleRoomHosting.Hosting.CheckOut && peopleRoomHosting.Hosting.CheckOut <= prh.Hosting.CheckOut);
}
