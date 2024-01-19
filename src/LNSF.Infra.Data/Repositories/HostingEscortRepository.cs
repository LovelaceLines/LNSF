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
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public HostingEscortRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoples = _context.Peoples.AsNoTracking();
        _patients = _context.Patients.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<HostingEscort>> Query(HostingEscortFilter filter)
    {
        var query = _hostingsEscorts;

        if (filter.HostingId.HasValue) query = QueryHostingId(query, filter.HostingId.Value);
        if (filter.EscortId.HasValue) query = QueryEscortId(query, filter.EscortId.Value);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(he => he.EscortId);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(he => he.EscortId);

        var hostingEscorts = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hostingEscorts;
    }

    public static IQueryable<HostingEscort> QueryHostingId(IQueryable<HostingEscort> query, int hostingId) =>
        query.Where(he => he.HostingId == hostingId);

    public static IQueryable<HostingEscort> QueryEscortId(IQueryable<HostingEscort> query, int escortId) =>
        query.Where(he => he.EscortId == escortId);

    public static IQueryable<HostingEscort> QueryActive(IQueryable<HostingEscort> query, bool active, IQueryable<Hosting> hostings) =>
        active ? query.Where(he => HostingRepository.QueryActive(hostings, active).Any(h => h.Id == he.HostingId)) :
            query.Where(he => !HostingRepository.QueryActive(hostings, active).Any(h => h.Id == he.HostingId));

    public async Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId) =>
        await _hostingsEscorts.AnyAsync(he => he.HostingId == hostingId && he.EscortId == escortId);

    public async Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId) =>
        await _hostingsEscorts.FirstOrDefaultAsync(he => he.HostingId == hostingId && he.EscortId == escortId) ??
            throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);

    public async Task<bool> ExistsWithDateConflict(int hostingId, int escortId)
    {
        var hosting = await _context.Hostings.FirstAsync(h => h.Id == hostingId);

        return await _hostingsEscorts.AnyAsync(he => he.EscortId == escortId && he.HostingId != hostingId &&
            (hosting.CheckIn < he.Hosting!.CheckIn || hosting.CheckIn < he.Hosting!.CheckOut) &&
            !(hosting.CheckIn == he.Hosting!.CheckIn && hosting.CheckOut == he.Hosting!.CheckOut));
    }
}
