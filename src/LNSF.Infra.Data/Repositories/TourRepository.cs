using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Enums;

namespace LNSF.Infra.Data.Repositories;

public class ToursRepository : BaseRepository<Tour>, ITourRepository
{
    private readonly AppDbContext _context;

    public ToursRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<List<Tour>> Query(TourFilter filter)
    {
        var query = _context.Tours.AsNoTracking();
        var count = await query.CountAsync();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Output != null) query = query.Where(x => x.Output >= filter.Output);
        if (filter.Input != null) query = query.Where(x => x.Input <= filter.Input);
        if (filter.InOpen == true) query = query.Where(x => x.Input == null);
        else if (filter.InOpen == false) query = query.Where(x => x.Input != null);
        if (filter.Note != null) query = query.Where(x => x.Note.ToLower().Contains(filter.Note.ToLower()));
        if (filter.PeopleId != null) query = query.Where(x => x.PeopleId == filter.PeopleId);
        if (filter.Order == OrderBy.Ascending) query = query.OrderBy(x => x.Output);
        else query = query.OrderByDescending(x => x.Output);

        var tours = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return tours;
    }

    public async Task<bool> IsClosed(int id) => 
        await _context.Tours.AsNoTracking()
            .Where(x => x.Id == id && x.Input != null)
            .AnyAsync();

    public async Task<bool> IsOpen(int id) => 
        await _context.Tours.AsNoTracking()
            .Where(x => x.Id == id && x.Input == null)
            .AnyAsync();
    
    public async Task<bool> PeopleHasOpenTour(int peopleId) => 
        await _context.Tours.AsNoTracking()
            .Where(x => x.PeopleId == peopleId && x.Input == null)
            .AnyAsync();
    
    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) => 
        await _context.Tours.AsNoTracking()
            .AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
