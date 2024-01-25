using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task Post_People_Ok()
    {
        var peopleFake = new PeoplePostViewModelFake().Generate();

        var countBefore = await GetCount(_peopleClient);
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);
        var countAfter = await GetCount(_peopleClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
    }

    [Theory]
    [InlineData("(##) #####-####")]
    [InlineData("####-####")]
    public async Task Post_PeopleWithValidPhoneNumber_OK(string phoneNumberFormat)
    {
        var peopleFake = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber(phoneNumberFormat)).Generate();

        var countBefore = await GetCount(_peopleClient);
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);
        var countAfter = await GetCount(_peopleClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
    }

    [Theory]
    [InlineData("(##) # ####-####")]
    [InlineData("(##) ####-####")]
    [InlineData("## (##) ####-####")]
    [InlineData("## (##) # ####-####")]
    public async Task Post_PeopleWithInvalidPhoneNumber_BadRequest(string phoneNumberFormat)
    {
        var peopleFake = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber(phoneNumberFormat)).Generate();

        var countBefore = await GetCount(_peopleClient);
        var exception = await Post<AppException>(_peopleClient, peopleFake);
        var countAfter = await GetCount(_peopleClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task Post_PeopleWithInvalidProps_BadRequest()
    {
        var peopleWithoutName = new PeoplePostViewModelFake(name: "").Generate();
        var peopleAged14 = new PeoplePostViewModelFake(birthDate: DateTime.Now.AddYears(-14)).Generate();
        var peopleAged129 = new PeoplePostViewModelFake(birthDate: DateTime.Now.AddYears(-129)).Generate();
        var peopleWithInvalidRG = new PeoplePostViewModelFake(rg: "123456789.").Generate();
        var peopleWithInvalidCPF = new PeoplePostViewModelFake(cpf: "123456789").Generate();
        var peopleWithInvalidPhone = new PeoplePostViewModelFake(phone: "123456789").Generate();
        var peopleWithInvalidEmail = new PeoplePostViewModelFake(email: "email").Generate();

        var countBefore = await GetCount(_peopleClient);
        var exceptionWithoutName = await Post<AppException>(_peopleClient, peopleWithoutName);
        var exceptionAged14 = await Post<AppException>(_peopleClient, peopleAged14);
        var exceptionAged129 = await Post<AppException>(_peopleClient, peopleAged129);
        var exceptionWithInvalidRG = await Post<AppException>(_peopleClient, peopleWithInvalidRG);
        var exceptionWithInvalidCPF = await Post<AppException>(_peopleClient, peopleWithInvalidCPF);
        var exceptionWithInvalidPhone = await Post<AppException>(_peopleClient, peopleWithInvalidPhone);
        var exceptionWithInvalidEmail = await Post<AppException>(_peopleClient, peopleWithInvalidEmail);
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
