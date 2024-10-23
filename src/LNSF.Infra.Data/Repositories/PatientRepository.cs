using AutoFilterer.Extensions;
using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PatientRepository(AppDbContext context, IMapper mapper, IHospitalRepository hospitalRepository, ITreatmentRepository treatmentRepository) : BaseRepository<Patient>(context), IPatientRepository
{
	public async Task<QueryResult<PatientDTO>> Query(PatientFilter filter)
	{
		var query = context.Patients.ApplyFilterWithoutPagination(filter);
		query = query.Include(x => x.People);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();

		var patientDTOs = mapper.Map<List<PatientDTO>>(items);
		patientDTOs.ForEach(async p =>
		{
			p.Hospital = await hospitalRepository.GetById(p.HospitalId);
			p.Treatments = await treatmentRepository.GetByPatientId(p.Id);
		});

		return new QueryResult<PatientDTO>(patientDTOs, totalCount);
	}

	public async Task<bool> ExistsByPeopleId(int peopleId) =>
		await context.Patients.AnyAsync(x => x.PeopleId == peopleId);

	public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
		await context.Patients.AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
