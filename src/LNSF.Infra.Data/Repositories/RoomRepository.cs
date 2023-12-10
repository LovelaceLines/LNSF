using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class RoomsRepository : BaseRepository<Room>, IRoomRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Room> _rooms;

    public RoomsRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _rooms = _context.Rooms.AsNoTracking();
    }

    public async Task<List<Room>> Query(RoomFilter filter)
    {
        var query = _rooms;

        if (filter.Id.HasValue) query = query.Where(r => r.Id == filter.Id);
        if (!filter.Number.IsNullOrEmpty()) query = query.Where(r => r.Number.ToLower() == filter.Number!.ToLower());
        if (filter.Bathroom.HasValue) query = query.Where(r => r.Bathroom);
        if (filter.Beds.HasValue) query = query.Where(r => r.Beds == filter.Beds);
        if (filter.Storey.HasValue) query = query.Where(r => r.Storey == filter.Storey);
        if (filter.Available.HasValue) query = query.Where(r => r.Available == filter.Available);

        if (filter.Order == OrderBy.Ascending) query = query.OrderBy(r => r.Number);
        else if (filter.Order == OrderBy.Descending) query = query.OrderByDescending(r => r.Number);

        var rooms = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return rooms;
    }

    public async Task<bool> ExistsByNumber(string number) => 
        await _rooms.AnyAsync(r => r.Number.ToLower() == number.ToLower());
}
