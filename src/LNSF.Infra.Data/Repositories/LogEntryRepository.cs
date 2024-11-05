using AutoFilterer.Extensions;
using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class LogEntryRepository(AppDbContext context) : BaseRepository<Room>(context), ILogEntryRepository
{
	public async Task<QueryResult<LogEntry>> Query(LogEntryFilter filter)
	{
		var query = context.LogEntries.ApplyFilterWithoutPagination(filter);
		query = query.Include(x => x.User);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<LogEntry>(items: items, totalCount: totalCount);
	}
}
