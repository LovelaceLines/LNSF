using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class TreatmentTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidTreatmentId_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();

        // Act - Treatment
        var queryId = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatment.Id));
        var treatmentIdQueried = queryId.First();

        Assert.Equivalent(treatment.Id, treatmentIdQueried.Id);
    }

    [Fact]
    public async Task Get_ValidTreatmentNameQuery_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();

        // Act - Treatment
        var queryName = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatment.Id, name: treatment.Name));
        var treatmentNameQueried = queryName.First();

        Assert.Equivalent(treatment, treatmentNameQueried);
    }

    [Fact]
    public async Task Get_ValidTreatmentTypeQuery_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();

        // Act - Treatment
        var queryType = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatment.Id, type: treatment.Type));
        var treatmentTypeQueried = queryType.First();

        Assert.Equivalent(treatment, treatmentTypeQueried);
    }

    [Fact]
    public async Task Get_ValidTreatmentGlobalQuery_Ok()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();

        // Act - Treatment
        var queryGlobalName = await Query<List<TreatmentViewModel>>(_treatmentClient, new TreatmentFilter(id: treatment.Id, globalFilter: treatment.Name));
        var treatmentGlobalNameQueried = queryGlobalName.First();

        Assert.Equivalent(treatment, treatmentGlobalNameQueried);
    }
}
