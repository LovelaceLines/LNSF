using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class HospitalRepository : BaseRepository<Hospital>, IHospitalRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Hospital> _hospitals;

    public HospitalRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _hospitals = _context.Hospitals.AsNoTracking();
    }

    public async Task<List<Hospital>> Query(HospitalFilter filter)
    {
        var query = _hospitals;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!);

        if (filter.Id.HasValue) query = QueryId(query, filter.Id.Value);
        if (!filter.Name.IsNullOrEmpty()) query = QueryName(query, filter.Name!);
        if (!filter.Acronym.IsNullOrEmpty()) query = QueryAcronym(query, filter.Acronym!);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(h => h.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(h => h.Name);

        var hospitals = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hospitals;
    }

    public static IQueryable<Hospital> QueryGlobalFilter(IQueryable<Hospital> query, string globalFilter) =>
        query.Where(h =>
            QueryName(query, globalFilter).Any(q => q.Id == h.Id) ||
            QueryAcronym(query, globalFilter).Any(q => q.Id == h.Id));

    public static IQueryable<Hospital> QueryId(IQueryable<Hospital> query, int id) =>
        query.Where(h => h.Id == id);

    public static IQueryable<Hospital> QueryName(IQueryable<Hospital> query, string name) =>
        query.Where(h => h.Name.ToLower().Contains(name.ToLower()));

    public static IQueryable<Hospital> QueryAcronym(IQueryable<Hospital> query, string acronym) =>
        query.Where(h => h.Acronym != null && h.Acronym.ToLower().Contains(acronym.ToLower()));

    public async Task<bool> ExistsByName(string name) =>
        await _hospitals.AnyAsync(h => h.Name.ToLower() == name.ToLower());
}
