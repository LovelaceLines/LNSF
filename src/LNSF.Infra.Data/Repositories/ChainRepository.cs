using AutoFilterer.Extensions;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LNSF.Infra.Data.Repositories;

public class ChainRepository(AppDbContext context) : IChainRepository
{
	public async Task<int> QueryCountPeopleHosted(ChainCountPeopleHostedFilter filter)
	{
		var date = filter.Date.ToDateTime(TimeOnly.MinValue);

		var countPeople = await context.PeoplesRoomsHostings.CountAsync(p =>
			p.Hosting!.CheckIn <= date &&
				(p.Hosting.CheckOut == null || date <= p.Hosting.CheckOut)
		);

		return countPeople;
	}

	public async Task<QueryResult<People>> QueryPeopleWillHosted(ChainDayFilter filter)
	{
		var date = DateTime.Now.Date.AddDays(filter.Days);

		var query = context.Peoples.Where(p =>
			context.Hostings.Any(h =>
				h.CheckIn >= DateTime.Now && h.CheckIn <= date &&
					(context.Patients.Any(pt => pt.PeopleId == p.Id && pt.Id == h.PatientId) ||
					context.HostingsEscorts.Any(he => he.HostingId == h.Id &&
						context.Escorts.Any(e => e.PeopleId == p.Id && e.Id == he.EscortId))
					)
			));

		var peoples = await query.ToListAsync();

		return new QueryResult<People>(items: peoples, totalCount: peoples.Count);
	}

	public async Task<List<PeopleRoomHosting>> QueryPeopleWillBirthday(ChainDayFilter filter)
	{

		var query = context.PeoplesRoomsHostings.Where(prh => prh.Hosting!.CheckIn <= DateTime.Now &&
				(prh.Hosting.CheckOut == null || DateTime.Now <= prh.Hosting.CheckOut)
			);

		var prh = await query.Include(prh => prh.People)
			.Include(prh => prh.Hosting)
			.ToListAsync();

		prh = prh.DistinctBy(prh => prh.PeopleId)
			.Where(prh =>
		{
			DateOnly maxDate = DateOnly.FromDateTime(DateTime.Now.Date).AddDays(filter.Days);
			DateOnly birthDate = prh.People!.BirthDate;
			DateOnly nextBirthday = new(DateTime.Now.Year, birthDate.Month, birthDate.Day);

			if (nextBirthday < DateOnly.FromDateTime(DateTime.Now.Date))
				nextBirthday = nextBirthday.AddYears(1);

			return nextBirthday <= maxDate;
		}).ToList();

		return prh ?? [];
	}

	public async Task<List<TreatmentDTO>> QueryCountTypeTreatment(ChainIntervalCheckFilter filter)
	{
		var checkIn = filter.CheckIn.ToDateTime(TimeOnly.MinValue);
		var checkOut = filter.CheckOut.ToDateTime(TimeOnly.MaxValue);

		FormattableString query = $@"
			SELECT
				t.*, COUNT(pt.TreatmentId) AS TotalCount
			FROM
				Treatments t
			INNER JOIN
				PatientsTreatments pt ON t.Id = pt.TreatmentId
			INNER JOIN
				Patients p ON pt.PatientId = p.Id
			INNER JOIN
				Hostings h ON p.Id = h.PatientId
			WHERE
				{checkIn} <= h.CheckIn AND h.CheckOut <= {checkOut}
			GROUP BY
				t.Type
			";

		var treatments = await context.Database
			.SqlQuery<TreatmentDTO>(query)
			.ToListAsync();

		return treatments;
	}
}
