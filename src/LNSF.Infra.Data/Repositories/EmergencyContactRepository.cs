using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactRepository(AppDbContext context) : BaseRepository<EmergencyContact>(context), IEmergencyContactRepository
{
	public async Task<QueryResult<EmergencyContact>> Query(EmergencyContactFilter filter)
	{
		var query = context.EmergencyContacts.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<EmergencyContact>(items: items, totalCount: totalCount);
	}

	public async Task<List<EmergencyContact>> GetByPeopleId(int peopleId) =>
		await context.EmergencyContacts.AsNoTracking()
			.Where(ec => ec.PeopleId == peopleId)
			.ToListAsync();

	public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
		await context.EmergencyContacts.AsNoTracking().AnyAsync(ec => ec.Id == id && ec.PeopleId == peopleId);
}
