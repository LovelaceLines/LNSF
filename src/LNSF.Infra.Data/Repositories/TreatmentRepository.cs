using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class TreatmentRepository(AppDbContext context) : BaseRepository<Treatment>(context), ITreatmentRepository
{
	public async Task<QueryResult<Treatment>> Query(TreatmentFilter filter)
	{
		var query = context.Treatments.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Treatment>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByName(string name) =>
		await context.Treatments.AnyAsync(t => t.Name == name);

	public async Task<bool> ExistsByNameAndType(string name, TypeTreatment type) =>
		await context.Treatments.AnyAsync(t => t.Name == name && t.Type == type);

	public async Task<List<Treatment>> GetByPatientId(int patientId) =>
		await context.PatientsTreatments
			.Where(pt => pt.PatientId == patientId)
			.Select(pt => pt.Treatment!)
			.ToListAsync();
}
