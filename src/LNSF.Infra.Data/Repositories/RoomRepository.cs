using AutoFilterer.Extensions;
using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class RoomRepository(AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
	public async Task<QueryResult<Room>> Query(RoomFilter filter)
	{
		var query = context.Rooms.ApplyFilterWithoutPagination(filter);

		if (filter.IsAvailable.HasValue)
		{
			if (filter.IsAvailable == true)
				query = query.Where(r => r.Available == filter.IsAvailable && !context.PeoplesRoomsHostings.Any(prh => prh.RoomId == r.Id &&
					prh.Hosting!.CheckIn <= DateTime.Now &&
						(DateTime.Now <= prh.Hosting.CheckOut || prh.Hosting.CheckOut == null)
				));

			if (filter.IsAvailable == false)
				query = query.Where(r => r.Available == filter.IsAvailable || context.PeoplesRoomsHostings.Any(prh => prh.RoomId == r.Id &&
					prh.Hosting!.CheckIn <= DateTime.Now &&
						(DateTime.Now <= prh.Hosting.CheckOut || prh.Hosting.CheckOut == null)
				));
		}

		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Room>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByNumber(string number) =>
		await context.Rooms.AnyAsync(r => r.Number.ToLower() == number.ToLower());
}
