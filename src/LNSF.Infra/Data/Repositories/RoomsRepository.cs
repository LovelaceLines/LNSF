using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class RoomsRepository : BaseRepository<Room>, IRoomsRepository
{
    private readonly AppDbContext _context;

    public RoomsRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Room>> Get(RoomFilters filters)
    {
        var query = _context.Rooms.AsNoTracking();
        var count = await query.CountAsync();

        if (filters.Id != null) query = query.Where(x => x.Id == filters.Id);
        if (filters.Number != null) query = query.Where(x => x.Number == filters.Number);
        if (filters.Bathroom != null) query = query.Where(x => x.Bathroom);
        if (filters.Beds != null) query = query.Where(x => x.Beds == filters.Beds);
        if (filters.Vacant != null) query = query.Where(x => x.Beds - x.Occupation > 0);
        if (filters.Storey != null) query = query.Where(x => x.Storey == filters.Storey);
        if (filters.Available != null) query = query.Where(x => x.Available == filters.Available);
        if (filters.Order == OrderBy.Ascending) query = query.OrderBy(x => x.Number);
        else query = query.OrderByDescending(x => x.Number);

        var rooms = await query
            .Skip((filters.Page.Page - 1) * filters.Page.PageSize)
            .Take(filters.Page.PageSize)
            .ToListAsync();

        return rooms;
    }
}
