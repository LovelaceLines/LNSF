using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class RoomsRepository : IRoomsRepository
{
    private readonly AppDbContext _context;

    public RoomsRepository(AppDbContext context) => 
        _context = context;

    public async Task<ResultDTO<List<Room>>> Get(RoomFilters filters)
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

        return new ResultDTO<List<Room>>(rooms);
    }

    public async Task<ResultDTO<Room>> Get(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        return (room == null) ?
            new ResultDTO<Room>("Não encontrado") :
            new ResultDTO<Room>(room);
    }

    public async Task<ResultDTO<int>> GetQuantity() =>
        new ResultDTO<int>(await _context.Rooms.CountAsync());

    public async Task<ResultDTO<Room>> Post(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        
        return new ResultDTO<Room>(room);
    }

    public async Task<ResultDTO<Room>> Put(Room room)
    {
        var _room = await _context.Rooms.FindAsync(room.Id);

        if (_room == null) return new ResultDTO<Room>("Não encontrado");

        _context.Entry(_room).CurrentValues.SetValues(room);

        _context.Rooms.Update(_room);
        await _context.SaveChangesAsync();

        return new ResultDTO<Room>(_room);
    }
}
