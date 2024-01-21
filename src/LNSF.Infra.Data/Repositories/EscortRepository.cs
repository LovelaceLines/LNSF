using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class EscortRepository : BaseRepository<Escort>, IEscortRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Escort> _escorts;

    public EscortRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoples = _context.Peoples.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
    }

    public async Task<List<Escort>> Query(EscortFilter filter)
    {
        var query = _escorts;
        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        var hostings = _context.Hostings.AsNoTracking();

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!, _peoples);

        if (filter.Id.HasValue) query = QueryId(query, filter.Id.Value);
        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value);

        if (filter.Active.HasValue) query = QueryActive(query, filter.Active.Value, hostings, hostingsEscorts);
        if (filter.IsVeteran.HasValue) query = QueryVeteran(query, filter.IsVeteran.Value, hostingsEscorts);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(e => e.Id);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(e => e.Id);

        var escorts = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPeople ?? false, _peoples))
            .ToListAsync();

        return escorts;
    }

    public static IQueryable<Escort> QueryGlobalFilter(IQueryable<Escort> query, string globalFilter, IQueryable<People> peoples) =>
        query.Where(e =>
            PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(p => p.Id == e.PeopleId));

    public static IQueryable<Escort> QueryId(IQueryable<Escort> query, int id) =>
        query.Where(e => e.Id == id);

    public static IQueryable<Escort> QueryPeopleId(IQueryable<Escort> query, int peopleId) =>
        query.Where(e => e.PeopleId == peopleId);

    public static IQueryable<Escort> QueryActive(IQueryable<Escort> query, bool active, IQueryable<Hosting> hostings, IQueryable<HostingEscort> hostingsEscorts) =>
        active ? query.Where(e => HostingEscortRepository.QueryActive(hostingsEscorts, active, hostings).Any(he => he.EscortId == e.Id)) :
            query.Where(e => !HostingEscortRepository.QueryActive(hostingsEscorts, active, hostings).Any(he => he.EscortId == e.Id));

    public static IQueryable<Escort> QueryVeteran(IQueryable<Escort> query, bool isVeteran, IQueryable<HostingEscort> hostingsEscorts) =>
        isVeteran ? query.Where(e => hostingsEscorts.Count(he => he.EscortId == e.Id) > 1) :
            query.Where(e => hostingsEscorts.Count(he => he.EscortId == e.Id) <= 1);

    public static Expression<Func<Escort, Escort>> Build(bool getPeople, IQueryable<People> peoples) =>
        e => new Escort
        {
            Id = e.Id,
            PeopleId = e.PeopleId,
            People = getPeople != true ? null : peoples.First(p => p.Id == e.PeopleId)
        };

    public async Task<bool> ExistsByPeopleId(int peopleId) =>
        await _escorts.AnyAsync(e => e.PeopleId == peopleId);

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _escorts.AnyAsync(e => e.Id == id && e.PeopleId == peopleId);
}
