using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class ToursRepository : BaseRepository<Tour>, ITourRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Tour> _tours;
    private readonly IQueryable<People> _peoples;

    public ToursRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _tours = _context.Tours.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
    }

    public async Task<List<Tour>> Query(TourFilter filter)
    {
        var query = _tours;

        if (filter.Id.HasValue) query = query.Where(t => t.Id == filter.Id);
        if (filter.Output.HasValue) query = query.Where(t => t.Output >= filter.Output);
        if (filter.Input.HasValue) query = query.Where(t => t.Input <= filter.Input);
        if (filter.PeopleId.HasValue) query = query.Where(t => t.PeopleId == filter.PeopleId);
        if (!filter.Note.IsNullOrEmpty()) query = query.Where(t => t.Note.ToLower().Contains(filter.Note!.ToLower()));
        
        if (filter.InOpen == true) query = query.Where(t => t.Input == null);
        else if (filter.InOpen == false) query = query.Where(t => t.Input != null);
        
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(t => t.Output);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(t => t.Output);

        var tours = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();
        
        if (filter.GetPeople == true)
            tours.ForEach(async t => t.People = await _peoples.FirstAsync(p => p.Id == t.PeopleId));

        return tours;
    }

    public async Task<bool> IsClosed(int id) => 
        await _tours.AnyAsync(t => t.Id == id && t.Input != null);

    public async Task<bool> IsOpen(int id) => 
        await _tours.AnyAsync(t => t.Id == id && t.Input == null);
    
    public async Task<bool> PeopleHasOpenTour(int peopleId) => 
        await _context.Tours.AsNoTracking()
            .Where(x => x.PeopleId == peopleId && x.Input == null)
            .AnyAsync();
    
    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) => 
        await _tours.AnyAsync(t => t.Id == id && t.PeopleId == peopleId);
}
