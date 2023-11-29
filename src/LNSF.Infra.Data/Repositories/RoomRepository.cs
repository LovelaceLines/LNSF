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

        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id);
        if (!string.IsNullOrEmpty(filter.Number)) query = query.Where(x => x.Number.ToLower() == filter.Number!.ToLower());
        if (filter.Bathroom.HasValue) query = query.Where(x => x.Bathroom);
        if (filter.Beds.HasValue) query = query.Where(x => x.Beds == filter.Beds);
        if (filter.Storey.HasValue) query = query.Where(x => x.Storey == filter.Storey);
        if (filter.Available.HasValue) query = query.Where(x => x.Available == filter.Available);
        if (filter.Order == OrderBy.Ascending) query = query.OrderBy(x => x.Number);
        else query = query.OrderByDescending(x => x.Number);

        var rooms = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return rooms;
    }

    public async Task<bool> ExistsByNumber(string number) => 
        await _context.Rooms.AsNoTracking()
            .AnyAsync(x => x.Number.ToLower() == number.ToLower());
}
