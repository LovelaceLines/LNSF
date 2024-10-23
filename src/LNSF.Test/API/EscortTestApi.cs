using LNSF.Domain.Entities;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class EscortTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Post_ValidEscort_Ok()
	{
		var people = await GetPeople();
		var escortFake = new Escort { PeopleId = people.Id };

		var escortPosted = await PostFromBody<Escort>(_escortClient, escortFake);

		Assert.Equal(escortFake.PeopleId, escortPosted.PeopleId);
	}

	[Fact]
	public async Task Post_EscortWithNotExistsPeopleId_NotFound()
	{
		var escortFake = new EscortFake(peopleId: 0).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_escortClient, escortFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_EscortWithUsedPeopleId_Conflict()
	{
		var escort = await GetEscort();
		var escortFake = new EscortFake(peopleId: escort.PeopleId).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_escortClient, escortFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public async Task Put_EscortWithOutherNonExistsPeopleId_NotFound(dynamic peopleId)
	{
		var escort = await GetEscort();
		var escortToPut = new EscortFake(id: escort.Id, peopleId: peopleId).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_escortClient, escortToPut);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Put_EscortWithUsePeopleId_NotFound()
	{
		var escort1 = await GetEscort();
		var escort2 = await GetEscort();
		var escortFake = new EscortFake(id: escort1.Id, peopleId: escort2.PeopleId).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_escortClient, escortFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Delete_ValidEscort_Ok()
	{
		var escort = await GetEscort();

		var escortDeleted = await DeleteFromUri<Escort>(_escortClient, escort.Id);

		Assert.Equal(escort.Id, escortDeleted.Id);
	}

	[Fact]
	public async Task Delete_EscortWithNotExistsId_NotFound()
	{
		var exception = await DeleteFromUri<AppHttpResponse>(_escortClient, 0);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}
}
