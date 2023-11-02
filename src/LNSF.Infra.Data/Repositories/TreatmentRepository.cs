using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class TreatmentRepository : BaseRepository<Treatment>, ITreatmentRepository
{
    private readonly AppDbContext _context;
    
    public TreatmentRepository(AppDbContext context) : base(context) => 
        _context = context;
    
    public async Task<List<Treatment>> Query(TreatmentFilter filter)
    {
        var query = _context.Treatments.AsNoTracking();
        
        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Name != null) query = query.Where(x => x.Name != null && x.Name.Contains(filter.Name));
        if (filter.Type != null) query = query.Where(x => x.Type == filter.Type);
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Name);
        else query = query.OrderByDescending(x => x.Name);

        var treatments = await query
            .Skip((filter.Page?.Page -1 ) * filter.Page?.PageSize ?? 0)
            .Take(filter.Page?.PageSize ?? 0)
            .ToListAsync();

        return treatments;
    }
}
