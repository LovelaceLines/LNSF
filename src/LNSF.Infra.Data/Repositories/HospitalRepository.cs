using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HospitalRepository : BaseRepository<Hospital>, IHospitalRepository
{
    private readonly AppDbContext _context;
    
    public HospitalRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Hospital>> Query(HospitalFilter filter)
    {
        var query = _context.Hospitals.AsNoTracking();
        
        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Name != null) query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (filter.Acronym != null) query = query.Where(x => x.Acronym != null && x.Acronym.ToLower().Contains(filter.Acronym.ToLower()));
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Name);
        else query = query.OrderByDescending(x => x.Name);

        var hospitals = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hospitals;
    }

    public async Task<bool> ExistsByName(string name) =>
        await _context.Hospitals.AsNoTracking()
            .AnyAsync(x => x.Name.ToLower() == name.ToLower());
}
