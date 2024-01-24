using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TreatmentTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_Treatment_Ok()
    {
        var treatment = await GetTreatment();
        var treatmentFake = new TreatmentViewModelFake(treatment.Id).Generate();

        var countBefore = await GetCount(_treatmentClient);
        var treatmentPuted = await Put<TreatmentViewModel>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(treatmentFake, treatmentPuted);
    }

    [Fact]
    public async Task Put_TreatmentWithRepeatedUniqueNameAndDifferentType_Ok()
    {
        var treatment = await GetTreatment();
        var treatmentFake = new TreatmentViewModelFake(treatment.Id, name: treatment.Name).Generate();

        var countBefore = await GetCount(_treatmentClient);
        var treatmentPuted = await Put<TreatmentViewModel>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(treatmentFake, treatmentPuted);
    }

    [Fact]
    public async Task Put_TreatmentWithRepeatedUniqueNamesAndType_Conflict()
    {
        var treatment1 = await GetTreatment();
        var treatment2 = await GetTreatment();
        var treatmentFake = new TreatmentViewModelFake(treatment1.Id, name: treatment2.Name, type: treatment2.Type).Generate();

        var countBefore = await GetCount(_treatmentClient);
        var exception = await Put<AppException>(_treatmentClient, treatmentFake);
        var countAfter = await GetCount(_treatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
