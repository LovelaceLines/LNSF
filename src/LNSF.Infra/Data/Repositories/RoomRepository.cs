using LNSF.Domain;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Room>> Get()
    {
        return await _context.Rooms.AsNoTracking().ToListAsync();
    }

    public async Task<Room> Get(int id)
    {
        var room = await _context.Rooms.FindAsync(id) 
            ?? throw new InvalidDataException("Não encontrado!");
        
        return room;
    }

    public async Task<Room> Add(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return room;
    }

    public async Task<Room> Update(Room room)
    {
        var _room = await _context.Rooms.FindAsync(room.Id) 
            ?? throw new InvalidDataException("Não encontrado!");

        _context.Entry(_room).CurrentValues.SetValues(room);

        _context.Rooms.Update(_room);
        await _context.SaveChangesAsync();

        return _room;
    }

    public async Task<bool> Available(int id)
    {
        var room = await _context.Rooms.FindAsync(id) 
            ?? throw new InvalidDataException("Não encontrado!");
        
        return room.Available ?? false;
    }

    public async Task<int> GetOccupation(int roomId)
    {
        var room = await _context.Rooms.FindAsync(roomId) 
            ?? throw new InvalidDataException("Não encontrado!");
        
        return room.Occupation ?? 0;
    }
}
