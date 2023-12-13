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
    private readonly IQueryable<Escort> _escorts;

    public EscortRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _escorts = _context.Escorts.AsNoTracking();
    }

    public async Task<List<Escort>> Query (EscortFilter filter)
    {
        var query = _escorts;
        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        var hostings = _context.Hostings.AsNoTracking();

        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id);
        if (filter.PeopleId.HasValue) query = query.Where(x => x.PeopleId == filter.PeopleId);

        if (filter.Active == true) query = query.Where(e =>
            hostingsEscorts.Any(he => he.EscortId == e.Id &&
                hostings.Any(h => h.Id == he.HostingId && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)));
        else if (filter.Active == false) query = query.Where(e =>
            !hostingsEscorts.Any(he => he.EscortId == e.Id &&
                hostings.Any(h => h.Id == he.HostingId && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)));
        
        if (filter.IsVeteran == true) query = query.Where(e =>
            hostingsEscorts.Count(he => he.EscortId == e.Id) > 1);
        else if (filter.IsVeteran == false) query = query.Where(e =>
            hostingsEscorts.Count(he => he.EscortId == e.Id) == 1);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Id);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.Id);

        var escorts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return escorts;
    }

    public async Task<bool> ExistsByPeopleId(int peopleId) =>
        await _escorts.AnyAsync(x => x.PeopleId == peopleId);

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _escorts.AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
