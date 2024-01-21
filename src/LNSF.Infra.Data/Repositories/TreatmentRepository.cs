using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class TreatmentRepository : BaseRepository<Treatment>, ITreatmentRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Treatment> _treatments;

    public TreatmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _treatments = _context.Treatments.AsNoTracking();
    }

    public async Task<List<Treatment>> Query(TreatmentFilter filter)
    {
        var query = _treatments;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!);

        if (filter.Id.HasValue) query = QueryId(query, filter.Id.Value);
        if (!filter.Name.IsNullOrEmpty()) query = QueryName(query, filter.Name!);
        if (filter.Type.HasValue) query = QueryType(query, filter.Type.Value);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(t => t.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(t => t.Name);

        var treatments = await query
            .Skip(filter.Page?.Page * filter.Page?.PageSize ?? 0)
            .Take(filter.Page?.PageSize ?? 0)
            .ToListAsync();

        return treatments;
    }

    public static IQueryable<Treatment> QueryGlobalFilter(IQueryable<Treatment> query, string globalFilter) =>
        query.Where(t => QueryName(query, globalFilter).Any(q => q.Id == t.Id));

    public static IQueryable<Treatment> QueryId(IQueryable<Treatment> query, int id) =>
        query.Where(t => t.Id == id);

    public static IQueryable<Treatment> QueryName(IQueryable<Treatment> query, string name) =>
        query.Where(t => t.Name != null && t.Name.ToLower().Contains(name.ToLower()));

    public static IQueryable<Treatment> QueryType(IQueryable<Treatment> query, TypeTreatment type) =>
        query.Where(t => t.Type == type);

    public async Task<bool> ExistsByName(string name) =>
        await _treatments.AnyAsync(t => t.Name == name);

    public async Task<bool> ExistsByNameAndType(string name, TypeTreatment type) =>
        await _treatments.AnyAsync(t => t.Name == name && t.Type == type);
}
