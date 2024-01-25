using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_People_Ok()
    {
        var people = await GetPeople();
        var peopleToPut = new PeopleViewModelFake(people.Id).Generate();

        var countBefore = await GetCount(_peopleClient);
        var peoplePuted = await Put<PeopleViewModel>(_peopleClient, peopleToPut);
        var countAfter = await GetCount(_peopleClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(peopleToPut, peoplePuted);
    }

    [Fact]
    public async Task Put_PeopleWithInvalidProps_BadRequest()
    {
        var people = await GetPeople();
        var peopleToPutWithoutName = new PeopleViewModelFake(people.Id, name: "").Generate();
        var peopleToPutAged14 = new PeopleViewModelFake(people.Id, birthDate: DateTime.Now.AddYears(-14)).Generate();
        var peopleToPutAged129 = new PeopleViewModelFake(people.Id, birthDate: DateTime.Now.AddYears(-129)).Generate();
        var peopleToPutWithInvalidRG = new PeopleViewModelFake(people.Id, rg: "123456789.").Generate();
        var peopleToPutWithInvalidCPF = new PeopleViewModelFake(people.Id, cpf: "123456789").Generate();
        var peopleToPutWithInvalidPhone = new PeopleViewModelFake(people.Id, phone: "123456789").Generate();
        var peopleToPutWithInvalidEmail = new PeopleViewModelFake(people.Id, email: "email").Generate();

        var countBefore = await GetCount(_peopleClient);
        var exceptionWithoutName = await Put<AppException>(_peopleClient, peopleToPutWithoutName);
        var exceptionAged14 = await Put<AppException>(_peopleClient, peopleToPutAged14);
        var exceptionAged129 = await Put<AppException>(_peopleClient, peopleToPutAged129);
        var exceptionWithInvalidRG = await Put<AppException>(_peopleClient, peopleToPutWithInvalidRG);
        var exceptionWithInvalidCPF = await Put<AppException>(_peopleClient, peopleToPutWithInvalidCPF);
        var exceptionWithInvalidPhone = await Put<AppException>(_peopleClient, peopleToPutWithInvalidPhone);
        var exceptionWithInvalidEmail = await Put<AppException>(_peopleClient, peopleToPutWithInvalidEmail);
        var countAfter = await GetCount(_peopleClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidRG.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidCPF.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidPhone.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidEmail.StatusCode);
    }
}
