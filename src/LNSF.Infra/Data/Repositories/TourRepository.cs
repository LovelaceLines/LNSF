using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Domain.Views;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class ToursRepository : ITourRepository
{
    private readonly AppDbContext _context;

    public ToursRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tour>> Get()
    {
        return await _context.Tours.AsNoTracking().ToListAsync();
    }

    public async Task<Tour> Get(int id)
    {
        var tour = await _context.Tours.FindAsync(id) 
            ?? throw new InvalidDataException("Não encontrado");

        return tour;
    }

    public async Task<Tour> AddOutput(ITourOutput output)
    {
        var tour = new Tour(output: output.Output, note: output.Note);

        await _context.Tours.AddAsync(tour);
        await _context.SaveChangesAsync();

        return tour; 
    }

    public async Task<Tour> AddInput(ITourInput input)
    {
        var tour = await _context.Tours.FindAsync(input.Id) 
            ?? throw new InvalidDataException("Não encontrado");
            
        _context.Entry(tour).CurrentValues.SetValues(input);

        _context.Tours.Update(tour);
        await _context.SaveChangesAsync();

        return tour;
    }
}
