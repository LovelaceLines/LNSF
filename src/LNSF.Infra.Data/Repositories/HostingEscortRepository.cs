using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

	public async Task<QueryResult<HostingEscort>> Query(BaseFilter filter)
	{
		var query = _hostingsEscorts.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<HostingEscort>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId) =>
		await _hostingsEscorts.AnyAsync(he => he.HostingId == hostingId && he.EscortId == escortId);

	public async Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId) =>
		await _hostingsEscorts.FirstOrDefaultAsync(he => he.HostingId == hostingId && he.EscortId == escortId) ??
			throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);

	// TODO - Refactor
	public async Task<bool> ExistsWithDateConflict(int hostingId, int escortId)
	{
		var hosting = await _context.Hostings.FirstAsync(h => h.Id == hostingId);

		return await _hostingsEscorts.AnyAsync(he => he.EscortId == escortId && he.HostingId != hostingId &&
			(hosting.CheckIn < he.Hosting!.CheckIn || hosting.CheckIn < he.Hosting!.CheckOut) &&
			!(hosting.CheckIn == he.Hosting!.CheckIn && hosting.CheckOut == he.Hosting!.CheckOut));
	}
}
