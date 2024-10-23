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
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();

		var peopleDTOs = mapper.Map<List<PeopleDTO>>(items);
		peopleDTOs.ForEach(async x =>
		{
			x.Tours = await tourRepository.GetByPeopleId(x.Id);
			x.EmergencyContacts = await emergencyContactRepository.GetByPeopleId(x.Id);
		});

		return new QueryResult<PeopleDTO>(items: peopleDTOs, totalCount: totalCount);
	}

	public Task<bool> ExistsByCpf(string cpf) =>
		context.Peoples.AnyAsync(x => x.CPF == cpf);

	public Task<bool> ExistsByEmail(string email) =>
		context.Peoples.AnyAsync(x => x.Email == email);

	public Task<bool> ExistsByPhone(string phone) =>
		context.Peoples.AnyAsync(x => x.Phone == phone);

	public Task<bool> ExistsByRg(string rg) =>
		context.Peoples.AnyAsync(x => x.RG == rg);
}
