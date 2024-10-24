using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EscortRepository(AppDbContext context) : BaseRepository<Escort>(context), IEscortRepository
{
	public async Task<QueryResult<Escort>> Query(EscortFilter filter)
	{
		var query = context.Escorts.ApplyFilterWithoutPagination(filter);
		query = query.Include(e => e.People);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Escort>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByPeopleId(int peopleId) =>
		await context.Escorts.AnyAsync(e => e.PeopleId == peopleId);

	public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
		await context.Escorts.AnyAsync(e => e.Id == id && e.PeopleId == peopleId);

	public async Task<List<Escort>> GetByHostingId(int hostingId) =>
		await context.HostingsEscorts.Where(he => he.HostingId == hostingId).Select(he => he.Escort!).ToListAsync();
}
