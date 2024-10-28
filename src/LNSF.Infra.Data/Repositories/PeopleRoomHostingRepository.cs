using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRoomHostingRepository(AppDbContext context) : BaseRepository<PeopleRoomHosting>(context), IPeopleRoomHostingRepository
{
	private readonly IQueryable<Room> rooms = context.Rooms.AsNoTracking();

	public async Task<QueryResult<PeopleRoomHosting>> Query(PeopleRoomHostingFilter filter)
	{
		var query = context.PeoplesRoomsHostings.ApplyFilterWithoutPagination(filter);
		query = query.Include(prh => prh.People).Include(prh => prh.Room).Include(prh => prh.Hosting);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<PeopleRoomHosting>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsHosting(PeopleRoomHosting peopleRoomHosting) =>
		await context.PeoplesRoomsHostings.AnyAsync(prh => prh.PeopleId == peopleRoomHosting.PeopleId &&
			prh.HostingId == peopleRoomHosting.HostingId);

	public async Task<bool> ExistsByPeopleRoomHosting(PeopleRoomHosting peopleRoomHosting) =>
		await context.PeoplesRoomsHostings.AnyAsync(prh => prh.PeopleId == peopleRoomHosting.PeopleId &&
			prh.RoomId == peopleRoomHosting.RoomId && prh.HostingId == peopleRoomHosting.HostingId);

	public async Task<bool> HaveVacancy(PeopleRoomHosting peopleRoomHosting)
	{
		var room = await rooms.FirstAsync(r => r.Id == peopleRoomHosting.RoomId);
		var beds = room.Beds;
		var occupation = await GetOccupation(peopleRoomHosting);

		return beds > occupation;
	}

	public async Task<int> GetOccupation(PeopleRoomHosting peopleRoomHosting) =>
		await context.PeoplesRoomsHostings.CountAsync(prh => prh.RoomId == peopleRoomHosting.RoomId &&
			prh.Hosting!.CheckIn <= peopleRoomHosting.Hosting!.CheckIn && peopleRoomHosting.Hosting.CheckIn <= prh.Hosting.CheckOut &&
			prh.Hosting.CheckIn <= peopleRoomHosting.Hosting.CheckOut && peopleRoomHosting.Hosting.CheckOut <= prh.Hosting.CheckOut);
}
