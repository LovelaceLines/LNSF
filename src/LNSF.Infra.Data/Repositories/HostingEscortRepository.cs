using System.Net;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingEscortRepository : BaseRepository<HostingEscort>, IHostingEscortRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public HostingEscortRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<HostingEscort>> Query(HostingEscortFilter filter) 
    {
        var query = _hostingsEscorts;

        if (filter.HostingId.HasValue) query = query.Where(he => he.HostingId == filter.HostingId);
        if (filter.EscortId.HasValue) query = query.Where(he => he.EscortId == filter.EscortId);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(he => he.EscortId);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(he => he.EscortId);

        var hostingEscorts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hostingEscorts;
    }

    public async Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId) => 
        await _hostingsEscorts.AnyAsync(he => he.HostingId == hostingId && he.EscortId == escortId);

    public async Task<List<HostingEscort>> GetByEscortId(int escortId) => 
        await _hostingsEscorts.Where(he => he.EscortId == escortId).ToListAsync();

    public async Task<List<HostingEscort>> GetByHostingId(int hostingId) => 
        await _hostingsEscorts.Where(he => he.HostingId == hostingId).ToListAsync();

    public async Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId) => 
        await _hostingsEscorts.FirstOrDefaultAsync(he => he.HostingId == hostingId && he.EscortId == escortId) ?? 
            throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);

    public Task<List<HostingEscort>> RemoveByEscortId(int escortId)
    {
        var hostingsEscorts = _hostingsEscorts.Where(he => he.EscortId == escortId);

        _context.HostingsEscorts.RemoveRange(hostingsEscorts);

        return hostingsEscorts.ToListAsync();
    }

    public Task<List<HostingEscort>> RemoveByHostingId(int hostingId)
    {
        var hostingsEscorts = _hostingsEscorts.Where(he => he.HostingId == hostingId);

        _context.HostingsEscorts.RemoveRange(hostingsEscorts);

        return hostingsEscorts.ToListAsync();
    }
}
