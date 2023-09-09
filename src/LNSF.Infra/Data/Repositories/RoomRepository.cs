using LNSF.Domain;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context) => 
        _context = context;

    public async Task<ResultDTO<List<Room>>> Get(Pagination pagination)
    {
        var query = _context.Rooms.AsNoTracking();
        var count = await query.CountAsync();

        if (count == 0) return new ResultDTO<List<Room>>("Não encontrado");
        
        var rooms = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
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
