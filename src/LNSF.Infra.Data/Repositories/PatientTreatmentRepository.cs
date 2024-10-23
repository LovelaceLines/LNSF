using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class PatientTreatmentRepository(AppDbContext context) : BaseRepository<PatientTreatment>(context), IPatientTreatmentRepository
{
	public async Task<QueryResult<PatientTreatment>> Query(BaseFilter filter)
	{
		var query = context.PatientsTreatments.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<PatientTreatment>(items: items, totalCount: totalCount);
	}

	public async Task<bool> ExistsByPatientIdAndTreatmentId(int patientId, int treatmentId) =>
		await context.PatientsTreatments.AnyAsync(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId);

	public async Task<PatientTreatment> GetByPatientIdAndTreatmentId(int patientId, int treatmentId) =>
		await context.PatientsTreatments.FirstOrDefaultAsync(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId) ??
			throw new AppException("Relacionamento Paciente e Tratamento não encontrado!", HttpStatusCode.NotFound);
}
