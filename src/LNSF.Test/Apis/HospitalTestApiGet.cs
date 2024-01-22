using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class HospitalTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidHospitalId_Ok()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();

        // Act - Hospital
        var queryId = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospital.Id));
        var hospitalIdQueried = queryId.First();

        // Assert
        Assert.Equivalent(hospital.Id, hospitalIdQueried.Id);
    }

    [Fact]
    public async Task Get_ValidHospitalName_Ok()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();

        // Act - Hospital
        var queryName = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospital.Id, name: hospital.Name));
        var hospitalNameQueried = queryName.First();

        // Assert
        Assert.Equivalent(hospital, hospitalNameQueried);
    }

    [Fact]
    public async Task Get_ValidHospitalAcronym_Ok()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();

        // Act - Hospital
        var queryAcronym = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospital.Id, acronym: hospital.Acronym));
        var hospitalAcronymQueried = queryAcronym.First();

        // Assert
        Assert.Equivalent(hospital, hospitalAcronymQueried);
    }

    [Fact]
    public async Task Get_ValidHospitalGlobalFilter_Ok()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();

        // Act - Hospital
        var queryGlobalFilterName = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospital.Id, globalFilter: hospital.Name));
        var hospitalGlobalFilterNameQueried = queryGlobalFilterName.First();

        var queryGlobalFilterAcronym = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospital.Id, globalFilter: hospital.Acronym));
        var hospitalGlobalFilterAcronymQueried = queryGlobalFilterAcronym.First();

        // Assert
        Assert.Equivalent(hospital, hospitalGlobalFilterNameQueried);
        Assert.Equivalent(hospital, hospitalGlobalFilterAcronymQueried);
    }
}
