using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TreatmentTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task Post_Treatment_Ok()
    {
        var treatmentFake = new TreatmentPostViewModelFake().Generate();

        var countBefore = await GetCount(_treatmentClient);
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(treatmentFake, treatmentPosted);
    }

    [Fact]
    public async Task Post_TreatmentWithRepeatedUniqueNameAndDifferentType_Ok()
    {
        var treatment = await GetTreatment();
        var treatmentFake = new TreatmentPostViewModelFake(name: treatment.Name).Generate();

        var countBefore = await GetCount(_treatmentClient);
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(treatmentFake, treatmentPosted);
    }

    [Fact]
    public async Task Post_TreatmentWithRepeatedUniqueNameAndType_Conflict()
    {
        var treatment = await GetTreatment();
        var treatmentFake = new TreatmentPostViewModelFake(name: treatment.Name, type: treatment.Type).Generate();

        var countBefore = await GetCount(_treatmentClient);
        var exception = await Post<AppException>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Theory]
    [InlineData("ab")]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
    public async Task Post_TreatmentWithShortAndLongName_BadRequest(string name)
    {
        var treatmentFake = new TreatmentPostViewModelFake(name: name).Generate();

        var countBefore = await GetCount(_treatmentClient);
        var exception = await Post<AppException>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
}
