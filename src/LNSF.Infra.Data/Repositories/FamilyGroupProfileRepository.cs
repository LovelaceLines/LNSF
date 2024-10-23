using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class FamilyGroupProfileRepository(AppDbContext context) : BaseRepository<FamilyGroupProfile>(context), IFamilyGroupProfileRepository
{
	public async Task<QueryResult<FamilyGroupProfile>> Query(FamilyGroupProfileFilter filter)
	{
		var query = context.FamilyGroupProfiles.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<FamilyGroupProfile>(items, totalCount);
	}
}
