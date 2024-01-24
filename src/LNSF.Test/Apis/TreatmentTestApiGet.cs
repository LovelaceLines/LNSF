using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class TreatmentTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_QueryTreatment_Ok()
    {
        var treatment = await GetTreatment();

        var hospitalQueried = await QueryFirst<TreatmentViewModel>(_treatmentClient, new TreatmentFilter(id: treatment.Id));

        Assert.Equivalent(treatment.Id, hospitalQueried.Id);
        Assert.Equivalent(treatment.Name, hospitalQueried.Name);
        Assert.Equivalent(treatment.Type, hospitalQueried.Type);
    }

    [Fact]
    public async Task Get_QueryTreatmentName_Ok()
    {
        var treatment = await GetTreatment();

        var treatmentNameQueried = await QueryFirst<TreatmentViewModel>(_treatmentClient, new TreatmentFilter(id: treatment.Id, name: treatment.Name));

        Assert.Equivalent(treatment.Name, treatmentNameQueried.Name);
    }

    [Fact]
    public async Task Get_QueryTreatmentType_Ok()
    {
        var treatment = await GetTreatment();

        var treatmentTypeQueried = await QueryFirst<TreatmentViewModel>(_treatmentClient, new TreatmentFilter(id: treatment.Id, type: treatment.Type));

        Assert.Equivalent(treatment.Type, treatmentTypeQueried.Type);
    }

    [Fact]
    public async Task Get_QueryTreatmentGlobalFilter_Ok()
    {
        var treatment = await GetTreatment();
        var treatmentQueried = await QueryFirst<TreatmentViewModel>(_treatmentClient, new TreatmentFilter(id: treatment.Id));

        var treatmentNameQueried = await QueryFirst<TreatmentViewModel>(_treatmentClient, new TreatmentFilter(id: treatment.Id, globalFilter: treatment.Name));

        Assert.Equivalent(treatmentQueried, treatmentNameQueried);
    }
}
