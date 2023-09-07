using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Domain.Views;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class ToorRepository : IToorRepository
{
    private readonly AppDbContext _context;

    public ToorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Toor>> Get()
    {
        return await _context.Toors.AsNoTracking().ToListAsync();
    }

    public async Task<Toor> Get(int id)
    {
        var toor = await _context.Toors.FindAsync(id) 
            ?? throw new InvalidDataException("Não encontrado");

        return toor;
    }

    public async Task<Toor> AddOutput(IToorOutput output)
    {
        var toor = new Toor(output: output.Output, note: output.Note);

        await _context.Toors.AddAsync(toor);
        await _context.SaveChangesAsync();

        return toor; 
    }

    public async Task<Toor> AddInput(IToorInput input)
    {
        var toor = await _context.Toors.FindAsync(input.Id) 
            ?? throw new InvalidDataException("Não encontrado");
            
        // toor.Input = input.Input;
        _context.Entry(toor).CurrentValues.SetValues(input);

        _context.Toors.Update(toor);
        await _context.SaveChangesAsync();

        return toor;
    }
}
