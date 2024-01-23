using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Room> _rooms;

    public RoomRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _rooms = _context.Rooms.AsNoTracking();
    }

    public async Task<List<Room>> Query(RoomFilter filter)
    {
        var query = _rooms;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobal(query, filter.GlobalFilter!);

        if (filter.Id.HasValue) query = QueryRoomId(query, filter.Id.Value);
        if (!filter.Number.IsNullOrEmpty()) query = QueryNumber(query, filter.Number!);
        if (filter.Bathroom.HasValue) query = QueryBathroom(query, filter.Bathroom.Value);
        if (filter.Beds.HasValue) query = QueryBeds(query, filter.Beds.Value);
        if (filter.Storey.HasValue) query = QueryStorey(query, filter.Storey.Value);
        if (filter.Available.HasValue) query = QueryAvailable(query, filter.Available.Value);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(r => r.Number);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(r => r.Number);

        var rooms = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return rooms;
    }

    public static IQueryable<Room> QueryGlobal(IQueryable<Room> query, string globalFilter) =>
        query.Where(r => QueryNumber(query, globalFilter).Any(q => q.Id == r.Id));

    public static IQueryable<Room> QueryRoomId(IQueryable<Room> query, int roomId) =>
        query.Where(r => r.Id == roomId);

    public static IQueryable<Room> QueryNumber(IQueryable<Room> query, string number) =>
        query.Where(r => r.Number.ToLower().Contains(number.ToLower()));

    public static IQueryable<Room> QueryBathroom(IQueryable<Room> query, bool bathroom) =>
        query.Where(r => r.Bathroom == bathroom);

    public static IQueryable<Room> QueryBeds(IQueryable<Room> query, int beds) =>
        query.Where(r => r.Beds == beds);

    public static IQueryable<Room> QueryStorey(IQueryable<Room> query, int storey) =>
        query.Where(r => r.Storey == storey);

    public static IQueryable<Room> QueryAvailable(IQueryable<Room> query, bool available) =>
        query.Where(r => r.Available == available);

    public async Task<bool> ExistsByNumber(string number) =>
        await _rooms.AnyAsync(r => r.Number.ToLower() == number.ToLower());
}
