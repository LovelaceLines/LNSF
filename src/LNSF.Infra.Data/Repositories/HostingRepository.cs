using AutoFilterer.Extensions;
using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingRepository(AppDbContext context, IMapper mapper, IEscortRepository escortRepository, IPeopleRepository peopleRepository) : BaseRepository<Hosting>(context), IHostingRepository
{
	public async Task<QueryResult<HostingDTO>> Query(HostingFilter filter)
	{
		var query = context.Hostings.ApplyFilterWithoutPagination(filter);
		query = query.Include(h => h.Patient);
		query = query.Include(h => h.Patient!.People);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();

		var hostingsDTOs = mapper.Map<List<HostingDTO>>(items);
		hostingsDTOs.ForEach(async h => h.Escorts = await escortRepository.GetByHostingId(h.Id));
		hostingsDTOs.ForEach(h => h.Escorts.ForEach(async e => e.People = await peopleRepository.GetById(e.PeopleId)));

		return new QueryResult<HostingDTO>(items: hostingsDTOs, totalCount: totalCount);
	}

	public async Task<bool> ExistsByIdAndPatientId(int id, int patientId) =>
		await context.Hostings.AnyAsync(h => h.Id == id && h.PatientId == patientId);

	public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
		await context.Hostings.AnyAsync(h => h.Id == id && (h.Patient!.PeopleId == peopleId ||
			context.HostingsEscorts.Any(he => he.HostingId == id &&
				context.Escorts.Any(e => e.Id == he.EscortId && e.PeopleId == peopleId))));

	public async Task<bool> ExistsWithDateConflict(Hosting hosting) =>
		await context.Hostings.AnyAsync(h => h.PatientId == hosting.PatientId && hosting.Id != h.Id &&
			(hosting.CheckIn < h.CheckIn || hosting.CheckIn <= h.CheckOut || h.CheckOut == null));
}
