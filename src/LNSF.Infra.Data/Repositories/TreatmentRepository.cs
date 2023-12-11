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
        
        if (filter.Id.HasValue) query = query.Where(t => t.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(t => t.Name != null && t.Name.ToLower().Contains(filter.Name!.ToLower()));
        if (filter.Type.HasValue) query = query.Where(t => t.Type == filter.Type);
        
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(t => t.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(t => t.Name);

        var treatments = await query
            .Skip((filter.Page?.Page -1 ) * filter.Page?.PageSize ?? 0)
            .Take(filter.Page?.PageSize ?? 0)
            .ToListAsync();

        return treatments;
    }

    public async Task<bool> ExistsByName(string name) => 
        await _treatments.AnyAsync(t => t.Name == name);

    public async Task<bool> ExistsByNameAndType(string name, TypeTreatment type) => 
        await _treatments.AnyAsync(t => t.Name == name && t.Type == type);
}
