using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EscortRepository : BaseRepository<Escort>, IEscortRepository
{
    private readonly AppDbContext _context;
    
    public EscortRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Escort>> Query (EscortFilter filter)
    {
        var query = _context.Escorts.AsNoTracking();

        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        var hostings = _context.Hostings.AsNoTracking();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.PeopleId != null) query = query.Where(x => x.PeopleId == filter.PeopleId);

        if (filter.Active == true) query = query.Where(e =>
            hostingsEscorts.Any(he => he.EscortId == e.Id &&
                hostings.Any(h => h.Id == he.HostingId && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)));
        else if (filter.Active == false) query = query.Where(e =>
            !hostingsEscorts.Any(he => he.EscortId == e.Id &&
                hostings.Any(h => h.Id == he.HostingId && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)));
        
        if (filter.IsVeteran == true) query = query.Where(e =>
            hostingsEscorts.Where(he => he.EscortId == e.Id).Count() > 1);
        else if (filter.IsVeteran == false) query = query.Where(e =>
            hostingsEscorts.Where(he => he.EscortId == e.Id).Count() == 1);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Id);
        else query = query.OrderByDescending(x => x.Id);

        var escorts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return escorts;
    }

    public async Task<bool> ExistsByPeopleId(int peopleId)
    {
        var escort = await _context.Escorts.AsNoTracking()
            .Where(x => x.PeopleId == peopleId)
            .FirstOrDefaultAsync();

        return escort != null;
    }

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _context.Escorts.AsNoTracking()
            .AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
    
}
