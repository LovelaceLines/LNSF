using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class TourRepository(AppDbContext context) : BaseRepository<Tour>(context), ITourRepository
{
	public async Task<QueryResult<Tour>> Query(TourFilter filter)
	{
		var query = context.Tours.ApplyFilterWithoutPagination(filter);

		if (filter.IsClose == true)
			query = query.Where(x => x.Input != null);

		if (filter.IsOpen == true)
			query = query.Where(x => x.Input == null);

		query = query.Include(x => x.People);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Tour>(items: items, totalCount: totalCount);
	}

	public async Task<List<Tour>> GetByPeopleId(int peopleId) =>
		await context.Tours.AsNoTracking()
			.Where(x => x.PeopleId == peopleId)
			.ToListAsync();

	public async Task<bool> IsClosed(int id) =>
		await context.Tours.AnyAsync(t => t.Id == id && t.Input != null);

	public async Task<bool> IsOpen(int id) =>
		await context.Tours.AnyAsync(t => t.Id == id && t.Input == null);

	public async Task<bool> PeopleHasOpenTour(int peopleId) =>
		await context.Tours.AsNoTracking()
			.Where(x => x.PeopleId == peopleId && x.Input == null)
			.AnyAsync();

	public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
		await context.Tours.AnyAsync(t => t.Id == id && t.PeopleId == peopleId);
}
