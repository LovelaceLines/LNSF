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

    public async Task<List<Escort>> Query(EscortFilter filter)
    {
        var query = _escorts;
        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        var hostings = _context.Hostings.AsNoTracking();

        if (filter.Id.HasValue) query = QueryId(query, filter.Id.Value);
        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value);

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
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        if (filter.GetPeople == true)
            escorts.ForEach(e => e.People = _context.Peoples.AsNoTracking()
                .FirstOrDefault(p => p.Id == e.PeopleId));

        return escorts;
    }

    public static IQueryable<Escort> QueryId(IQueryable<Escort> query, int id) =>
        query.Where(x => x.Id == id);

    public static IQueryable<Escort> QueryPeopleId(IQueryable<Escort> query, int peopleId) =>
        query.Where(x => x.PeopleId == peopleId);

    public static IQueryable<Escort> QueryActive(IQueryable<Escort> query, bool active, IQueryable<Hosting> hostings, IQueryable<HostingEscort> hostingsEscorts) =>
        active ? query.Where(e => HostingEscortRepository.QueryActive(hostingsEscorts, active, hostings).Any(he => he.EscortId == e.Id)) :
            query.Where(e => !HostingEscortRepository.QueryActive(hostingsEscorts, active, hostings).Any(he => he.EscortId == e.Id));

    public async Task<bool> ExistsByPeopleId(int peopleId) =>
        await _escorts.AnyAsync(x => x.PeopleId == peopleId);

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _escorts.AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
