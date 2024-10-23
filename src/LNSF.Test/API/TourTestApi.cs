using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class TourTestApiPost : GlobalClientRequest
{
	[Fact]
	public async Task Get_QueryOpenTour_Ok()
	{
		var tour = await GetTour();

		var result = await GetFromQuery<QueryResult<Tour>>(_tourClient, new TourFilter
		{
			Id = tour.Id,
			PeopleId = tour.PeopleId,
		});
		var tourQueried = result.Items.Single();

		Assert.Equal(tour.Id, tourQueried.Id);
		Assert.Equal(tour.PeopleId, tourQueried.PeopleId);
		Assert.Equal(tour.Output.Date, tourQueried.Output.Date);
		Assert.Equal(tour.Output.Hour, tourQueried.Output.Hour);
		Assert.Equal(tour.Output.Minute, tourQueried.Output.Minute);
		Assert.Equal(tour.Note, tourQueried.Note);
	}

	[Fact]
	public async Task Get_QueryCloseTour_Ok()
	{
		var openTour = await GetTour();
		var closeTour = await GetTour(openTourId: openTour.Id, peopleId: openTour.PeopleId);

		var result = await GetFromQuery<QueryResult<Tour>>(_tourClient, new TourFilter
		{
			Id = closeTour.Id,
			PeopleId = closeTour.PeopleId,
		});
		var tourQueried = result.Items.Single();

		Assert.Equal(closeTour.Id, tourQueried.Id);
		Assert.Equal(closeTour.PeopleId, tourQueried.PeopleId);
		Assert.Equal(closeTour.Output.Date, tourQueried.Output.Date);
		Assert.Equal(closeTour.Output.Hour, tourQueried.Output.Hour);
		Assert.Equal(closeTour.Output.Minute, tourQueried.Output.Minute);
		Assert.Equal(closeTour.Input!.Value.Date, tourQueried.Input!.Value.Date);
		Assert.Equal(closeTour.Input.Value.Hour, tourQueried.Input.Value.Hour);
		Assert.Equal(closeTour.Input.Value.Minute, tourQueried.Input.Value.Minute);
		Assert.Equal(closeTour.Note, tourQueried.Note);
	}

	[Fact]
	public async Task Post_Tour_Ok()
	{
		var people = await GetPeople();
		var fake = new TourFake(peopleId: people.Id).Generate();

		var tour = await PostFromBody<Tour>(_tourClient, fake);

		Assert.Equal(fake.PeopleId, tour.PeopleId);
		Assert.Equal(fake.Output.Date, tour.Output.Date);
		Assert.Equal(fake.Output.Hour, tour.Output.Hour);
		Assert.Equal(fake.Output.Minute, tour.Output.Minute);
		Assert.Equal(fake.Note, tour.Note);
	}

	[Fact]
	public async Task Post_TourWithPeopleHaveAnOpenTour_Conflict()
	{
		var openTour = await GetTour();
		var openTourFake = new TourFake(peopleId: openTour.PeopleId).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_tourClient, openTourFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task Put_Tour_Ok()
	{
		var openTour = await GetTour();
		var closeTourFake = new TourFake(id: openTour.Id, peopleId: openTour.PeopleId).Generate();

		var tourPuted = await PutFromBody<Tour>(_putAllClient, closeTourFake);

		Assert.Equal(closeTourFake.Id, tourPuted.Id);
		Assert.Equal(closeTourFake.PeopleId, tourPuted.PeopleId);
		Assert.Equal(closeTourFake.Input!.Value.Date, tourPuted.Input!.Value.Date);
		Assert.Equal(closeTourFake.Input.Value.Hour, tourPuted.Input.Value.Hour);
		Assert.Equal(closeTourFake.Input.Value.Minute, tourPuted.Input.Value.Minute);
		Assert.Equal(closeTourFake.Note, tourPuted.Note);
	}

	[Fact]
	public async Task Put_TourWithNonExistentPeopleId_NotFound()
	{
		var openTour = await GetTour();
		var closeTourFake = new TourFake(id: openTour.Id, peopleId: -1).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_putAllClient, closeTourFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Put_ValidOpenTour_Ok()
	{
		var openTour = await GetTour();
		var closeTourFake = new TourFake(id: openTour.Id, peopleId: openTour.PeopleId).Generate();

		var closeTourPuted = await PutFromBody<Tour>(_putAllClient, closeTourFake);

		Assert.Equal(closeTourFake.Id, closeTourPuted.Id);
		Assert.Equal(closeTourFake.PeopleId, closeTourPuted.PeopleId);
		Assert.Equal(closeTourFake.Output.Date, closeTourPuted.Output.Date);
		Assert.Equal(closeTourFake.Output.Hour, closeTourPuted.Output.Hour);
		Assert.Equal(closeTourFake.Output.Minute, closeTourPuted.Output.Minute);
		Assert.Equal(closeTourFake.Input!.Value.Date, closeTourPuted.Input!.Value.Date);
		Assert.Equal(closeTourFake.Input.Value.Hour, closeTourPuted.Input.Value.Hour);
		Assert.Equal(closeTourFake.Input.Value.Minute, closeTourPuted.Input.Value.Minute);
		Assert.Equal(closeTourFake.Note, closeTourPuted.Note);
	}

	[Fact]
	public async Task Put_ValidCloseTour_Ok()
	{
		var openTour = await GetTour();
		var closeTour = await GetTour(openTourId: openTour.Id, peopleId: openTour.PeopleId);
		var closeTourToPutAllFake = new TourFake(closeTour.Id, closeTour.PeopleId).Generate();

		var closeTourPuted = await PutFromBody<Tour>(_putAllClient, closeTourToPutAllFake);

		Assert.Equal(closeTourToPutAllFake.Id, closeTourPuted.Id);
		Assert.Equal(closeTourToPutAllFake.PeopleId, closeTourPuted.PeopleId);
		Assert.Equal(closeTourToPutAllFake.Output.Date, closeTourPuted.Output.Date);
		Assert.Equal(closeTourToPutAllFake.Output.Hour, closeTourPuted.Output.Hour);
		Assert.Equal(closeTourToPutAllFake.Output.Minute, closeTourPuted.Output.Minute);
		Assert.Equal(closeTourToPutAllFake.Input!.Value.Date, closeTourPuted.Input!.Value.Date);
		Assert.Equal(closeTourToPutAllFake.Input.Value.Hour, closeTourPuted.Input.Value.Hour);
		Assert.Equal(closeTourToPutAllFake.Input.Value.Minute, closeTourPuted.Input.Value.Minute);
		Assert.Equal(closeTourToPutAllFake.Note, closeTourPuted.Note);
	}

	[Fact]
	public async Task Put_InvalidTourWithInvalidDates_BadRequest()
	{
		var openTour = await GetTour();
		var closeTour = await GetTour(openTourId: openTour.Id, peopleId: openTour.PeopleId);
		var closeTourToPutAllFake = new TourFake(id: closeTour.Id, peopleId: closeTour.PeopleId, output: new Bogus.DataSets.Date().Future(), input: new Bogus.DataSets.Date().Past()).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_putAllClient, closeTourToPutAllFake);

		Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
	}
}
