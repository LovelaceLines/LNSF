using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class ServiceRecordRepository(AppDbContext context) : BaseRepository<ServiceRecord>(context), IServiceRecordRepository
{
	public async Task<QueryResult<ServiceRecord>> Query(BaseFilter filter)
	{
		var query = context.ServiceRecords.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<ServiceRecord>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsById(int id, int patientId) =>
		await context.ServiceRecords.AnyAsync(s => s.Id == id && s.PatientId == patientId);

	public async Task<bool> ExistsByPatientId(int patientId) =>
		await context.ServiceRecords.AnyAsync(s => s.PatientId == patientId);
}
