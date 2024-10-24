using AutoFilterer.Extensions;
using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRepository(AppDbContext context,
	IEmergencyContactRepository emergencyContactRepository,
	IMapper mapper,
	ITourRepository tourRepository) : BaseRepository<People>(context), IPeopleRepository
{
	public async Task<QueryResult<PeopleDTO>> Query(PeopleFilter filter)
	{
		var query = context.Peoples.ApplyFilterWithoutPagination(filter);

		if (filter.IsPatient == true)
			query = query.Where(p => context.Patients.Any(pt => pt.PeopleId == p.Id));

		if (filter.IsEscort == true)
			query = query.Where(p => context.Escorts.Any(e => e.PeopleId == p.Id));

		if (filter.IsActive == true)
			query = query.Where(p => context.PeoplesRoomsHostings.Any(prh => prh.PeopleId == p.Id && prh.Hosting!.CheckIn <= DateTime.Now && prh.Hosting.CheckOut >= DateTime.Now));

		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();

		var peopleDTOs = mapper.Map<List<PeopleDTO>>(items);
		peopleDTOs.ForEach(async p =>
		{
			p.Experience = (await GetHostingsCountByPeopleId(p.Id)) > 0 ? "Veterano" : "Novato";
			p.Status = await GetStatusByPeopleId(p.Id);
			p.Tours = await tourRepository.GetByPeopleId(p.Id);
			p.EmergencyContacts = await emergencyContactRepository.GetByPeopleId(p.Id);
		});

		return new QueryResult<PeopleDTO>(items: peopleDTOs, totalCount: totalCount);
	}

	private async Task<int> GetHostingsCountByPeopleId(int peopleId) =>
		await context.PeoplesRoomsHostings.CountAsync(prh => prh.PeopleId == peopleId);

	private async Task<string> GetStatusByPeopleId(int peopleId) =>
		(await context.Patients.AnyAsync(p => p.PeopleId == peopleId)) ? "Paciente" : (await context.Escorts.AnyAsync(e => e.PeopleId == peopleId)) ? "Acompanhante" : "Sem status";

	public Task<bool> ExistsByCpf(string cpf) =>
		context.Peoples.AnyAsync(x => x.CPF == cpf);

	public Task<bool> ExistsByEmail(string email) =>
		context.Peoples.AnyAsync(x => x.Email == email);

	public Task<bool> ExistsByPhone(string phone) =>
		context.Peoples.AnyAsync(x => x.Phone == phone);

	public Task<bool> ExistsByRg(string rg) =>
		context.Peoples.AnyAsync(x => x.RG == rg);
}
