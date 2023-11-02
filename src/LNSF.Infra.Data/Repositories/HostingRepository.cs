using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingRepository : BaseRepository<Hosting>, IHostingRepository
{
    private readonly AppDbContext _context;

    public HostingRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Hosting>> Query(HostingFilter filter)
    {
        var query = _context.Hostings.AsNoTracking();
        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id)
        if (filter.PatientId != null) query = query.Where(x => x.PatientId == filter.PatientId)
        if (filter.CheckIn != null) query = query.Where(x => x.CheckIn >= filter.CheckIn)
        if (filter.CheckOut != null) query = query.Where(x => x.CheckOut <= filter.CheckOut)
        if (filter.OrderBy == OrderBy.Ascendent) query = query.OrderBy(x => x.Id)
        else query = query.OrderByDescendent (x => x.Id)

        var hostings = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hostings;
    }
}