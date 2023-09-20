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

        query = query.Where(x => x.Bathroom);
        query = query.Where(x => x.Beds == filters.Beds);
        query = query.Where(x => x.Beds - x.Occupation > 0);
        query = query.Where(x => x.Storey == filters.Storey);
        query = query.Where(x => x.Available);
        if (filters.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.Id);

        var rooms = await query
            .Skip((filters.Page.Page - 1) * filters.Page.PageSize)
            .Take(filters.Page.PageSize)
            .ToListAsync();

        return rooms;
    }
}
