using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Enums;

namespace LNSF.Infra.Data.Repositories;

public class RoomsRepository : BaseRepository<Room>, IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomsRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Room>> Query(RoomFilter filter)
    {
        var query = _context.Rooms.AsNoTracking();
        var count = await query.CountAsync();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Number != null) query = query.Where(x => x.Number == filter.Number);
        if (filter.Bathroom != null) query = query.Where(x => x.Bathroom);
        if (filter.Beds != null) query = query.Where(x => x.Beds == filter.Beds);
        if (filter.Vacant != null) query = query.Where(x => x.Beds - x.Occupation > 0);
        if (filter.Storey != null) query = query.Where(x => x.Storey == filter.Storey);
        if (filter.Available != null) query = query.Where(x => x.Available == filter.Available);
        if (filter.Order == OrderBy.Ascending) query = query.OrderBy(x => x.Number);
        else query = query.OrderByDescending(x => x.Number);

        var rooms = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return rooms;
    }
}
