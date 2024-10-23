using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HospitalRepository(AppDbContext context) : BaseRepository<Hospital>(context), IHospitalRepository
{
	public async Task<QueryResult<Hospital>> Query(HospitalFilter filter)
	{
		var query = context.Hospitals.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Hospital>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByName(string name) =>
		await context.Hospitals.AnyAsync(h => h.Name.ToLower() == name.ToLower());
}
