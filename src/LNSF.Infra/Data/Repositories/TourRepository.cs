using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Domain.Views;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class ToursRepository : ITourRepository
{
    private readonly AppDbContext _context;

    public ToursRepository(AppDbContext context) =>
        _context = context;

    public async Task<ResultDTO<List<Tour>>> Get(Pagination pagination)
    {
        var query = _context.Tours.AsNoTracking();
        var count = await query.CountAsync();

        if (count == 0) return new ResultDTO<List<Tour>>("Não encontrado");

        var tours = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new ResultDTO<List<Tour>>(tours);
    }

    public async Task<ResultDTO<Tour>> Get(int id)
    {
        var tour = await _context.Tours.FindAsync(id);

        return tour == null ? 
            new ResultDTO<Tour>("Não encontrado") :
            new ResultDTO<Tour>(tour);
    }

    public async Task<ResultDTO<int>> GetQuantityTours() =>
        new ResultDTO<int>(await _context.Tours.CountAsync());
    
    public async Task<ResultDTO<Tour>> PostOutput(Tour tour)
    {
        await _context.Tours.AddAsync(tour);
        await _context.SaveChangesAsync();

        return new ResultDTO<Tour>(tour); 
    }

    public async Task<ResultDTO<Tour>> PutInput(Tour tour)
    {
        var _tour = await _context.Tours.FindAsync(tour.Id);

        if (_tour == null) return new ResultDTO<Tour>("Não encontrado");
            
        _context.Entry(_tour).CurrentValues.SetValues(tour);

        _context.Tours.Update(_tour);
        await _context.SaveChangesAsync();

        return new ResultDTO<Tour>(_tour);
    }
}
