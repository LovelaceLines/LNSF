using AutoFilterer.Extensions;
using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class RoomRepository(AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
	private readonly DbSet<Room> _rooms = context.Rooms;

	public async Task<QueryResult<Room>> Query(RoomFilter filter)
	{
		var query = _rooms.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Room>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByNumber(string number) =>
		await _rooms.AnyAsync(r => r.Number.ToLower() == number.ToLower());
}
