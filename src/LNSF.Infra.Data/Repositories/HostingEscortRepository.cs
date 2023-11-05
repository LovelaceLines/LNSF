using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingEscortRepository : BaseRepository<HostingEscort>, IHostingEscortRepository
{
    private readonly AppDbContext _context;

    public HostingEscortRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId) => 
        await _context.HostingsEscorts
            .FindAsync(new HostingEscort { HostingId = hostingId, EscortId = escortId }) != null;

    public async Task<List<HostingEscort>> GetByEscortId(int escortId) => 
        await _context.HostingsEscorts
            .Where(he => he.EscortId == escortId)
            .ToListAsync();

    public Task<List<HostingEscort>> GetByHostingId(int hostingId) => 
        _context.HostingsEscorts
            .Where(he => he.HostingId == hostingId)
            .ToListAsync();

    public async Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId) => 
        await _context.HostingsEscorts
            .FindAsync(new HostingEscort { HostingId = hostingId, EscortId = escortId })
                ?? throw new Exception("HostingEscort not found");

    public Task<List<HostingEscort>> RemoveByEscortId(int escortId)
    {
        var hostingsEscorts = _context.HostingsEscorts.Where(he => he.EscortId == escortId);

        _context.HostingsEscorts.RemoveRange(hostingsEscorts);

        return hostingsEscorts.ToListAsync();
    }

    public Task<List<HostingEscort>> RemoveByHostingId(int hostingId)
    {
        var hostingsEscorts = _context.HostingsEscorts.Where(he => he.HostingId == hostingId);

        _context.HostingsEscorts.RemoveRange(hostingsEscorts);

        return hostingsEscorts.ToListAsync();
    }
}
