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
        
        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(x => x.Name.ToLower().Contains(filter.Name!.ToLower()));
        if (!filter.Acronym.IsNullOrEmpty()) query = query.Where(x => x.Acronym != null && x.Acronym.ToLower().Contains(filter.Acronym!.ToLower()));
        
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.Name);

        var hospitals = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hospitals;
    }

    public async Task<bool> ExistsByName(string name) =>
        await _hospitals.AnyAsync(x => x.Name.ToLower() == name.ToLower());
}
