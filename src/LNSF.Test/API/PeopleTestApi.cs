using LNSF.Domain.Entities;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class PeopleTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Post_People_Ok()
	{
		var fake = new PeopleFake().Generate();

		var people = await PostFromBody<People>(_peopleClient, fake);

		Assert.Equal(fake.RG, people.RG);
		Assert.Equal(fake.CPF, people.CPF);
	}

	[Fact]
	public async Task Post_PeopleWithInvalidProps_BadRequest()
	{
		var people = await GetPeople();
		var peopleWithoutNameFake = new PeopleFake(name: "").Generate();
		var peopleAged14Fake = new PeopleFake(birthDate: DateOnly.FromDateTime(DateTime.Now.AddYears(-14))).Generate();
		var peopleAged129Fake = new PeopleFake(birthDate: DateOnly.FromDateTime(DateTime.Now.AddYears(-129))).Generate();
		var peopleWithInvalidRGFake = new PeopleFake(rg: people.RG).Generate();
		var peopleWithInvalidCPFFake = new PeopleFake(cpf: people.CPF).Generate();
		var peopleWithInvalidPhoneFake = new PeopleFake(phone: people.Phone).Generate();
		var peopleWithInvalidEmailFake = new PeopleFake(email: people.Email).Generate();

		var exceptionWithoutName = await PostFromBody<AppHttpResponse>(_peopleClient, peopleWithoutNameFake);
		var exceptionAged14 = await PostFromBody<AppHttpResponse>(_peopleClient, peopleAged14Fake);
		var exceptionAged129 = await PostFromBody<AppHttpResponse>(_peopleClient, peopleAged129Fake);
		var exceptionWithInvalidRG = await PostFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidRGFake);
		var exceptionWithInvalidCPF = await PostFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidCPFFake);
		var exceptionWithInvalidPhone = await PostFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidPhoneFake);
		var exceptionWithInvalidEmail = await PostFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidEmailFake);

		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidRG.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidCPF.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidPhone.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidEmail.StatusCode);
	}

	[Fact]
	public async Task Put_People_Ok()
	{
		var people = await GetPeople();
		var fake = new PeopleFake(people.Id).Generate();

		var peoplePuted = await PutFromBody<People>(_peopleClient, fake);

		Assert.Equal(fake.Id, peoplePuted.Id);
		Assert.Equal(fake.RG, peoplePuted.RG);
		Assert.Equal(fake.CPF, peoplePuted.CPF);
		Assert.Equal(fake.Name, peoplePuted.Name);
		Assert.Equal(fake.BirthDate, peoplePuted.BirthDate);
		Assert.Equal(fake.Phone, peoplePuted.Phone);
		Assert.Equal(fake.Email, peoplePuted.Email);
		Assert.Equal(fake.Street, peoplePuted.Street);
		Assert.Equal(fake.City, peoplePuted.City);
		Assert.Equal(fake.State, peoplePuted.State);
		Assert.Equal(fake.HouseNumber, peoplePuted.HouseNumber);
		Assert.Equal(fake.Neighborhood, peoplePuted.Neighborhood);
		Assert.Equal(fake.MaritalStatus, peoplePuted.MaritalStatus);
		Assert.Equal(fake.RaceColor, peoplePuted.RaceColor);
	}

	[Fact]
	public async Task Put_PeopleWithInvalidProps_BadRequest()
	{
		var people1 = await GetPeople();
		var people2 = await GetPeople();
		var peopleWithoutName = new PeopleFake(people1.Id, name: "").Generate();
		var peopleAged14 = new PeopleFake(people1.Id, birthDate: DateOnly.FromDateTime(DateTime.Now.AddYears(-14))).Generate();
		var peopleAged129 = new PeopleFake(people1.Id, birthDate: DateOnly.FromDateTime(DateTime.Now.AddYears(-129))).Generate();
		var peopleWithInvalidRG = new PeopleFake(people1.Id, rg: people2.RG).Generate();
		var peopleWithInvalidCPF = new PeopleFake(people1.Id, cpf: people2.CPF).Generate();
		var peopleWithInvalidPhone = new PeopleFake(people1.Id, phone: people2.Phone).Generate();
		var peopleWithInvalidEmail = new PeopleFake(people1.Id, email: people2.Email).Generate();

		var exceptionWithoutName = await PutFromBody<AppHttpResponse>(_peopleClient, peopleWithoutName);
		var exceptionAged14 = await PutFromBody<AppHttpResponse>(_peopleClient, peopleAged14);
		var exceptionAged129 = await PutFromBody<AppHttpResponse>(_peopleClient, peopleAged129);
		var exceptionWithInvalidRG = await PutFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidRG);
		var exceptionWithInvalidCPF = await PutFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidCPF);
		var exceptionWithInvalidPhone = await PutFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidPhone);
		var exceptionWithInvalidEmail = await PutFromBody<AppHttpResponse>(_peopleClient, peopleWithInvalidEmail);

		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidRG.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidCPF.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidPhone.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionWithInvalidEmail.StatusCode);
	}
}
