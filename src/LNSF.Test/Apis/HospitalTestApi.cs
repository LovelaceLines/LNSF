using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HospitalTestApi : GlobalClientRequest
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


    [Fact]
    public async Task Post_HospitalValid_Ok()
    {
        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Act - Query
        var query = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospitalPosted.Id));
        var hospitalQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hospitalFake, hospitalPosted);
        Assert.Equivalent(hospitalPosted, hospitalQueried);
    }

    [Fact]
    public async Task Post_HospitalInvalid_BadRequest()
    {
        // Arrange - Hospital
        var hospitalWithInvalidName = new HospitalPostViewModelFake(name: "hosp").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        var exceptionWithInvalidName = await Post<AppException>(_hospitalClient, hospitalWithInvalidName);

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidName.StatusCode);
    }

    [Fact]
    public async Task Post_HospitalInvalidWithUniqueName_Conflict()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();
        var hospitalFake = new HospitalPostViewModelFake(name: hospital.Name).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        var exception = await Post<AppException>(_hospitalClient, hospitalFake);

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task Put_HospitalValid_Ok()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();
        var hospitalToPut = new HospitalViewModelFake(id: hospital.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        var hospitalPuted = await Put<HospitalViewModel>(_hospitalClient, hospitalToPut);

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Act - Query
        var query = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospitalPuted.Id));
        var hospitalQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(hospitalPuted, hospitalToPut);
        Assert.Equivalent(hospitalPuted, hospitalQueried);
    }

    [Fact]
    public async Task Put_HospitalInvalidWithUniqueName_Ok()
    {
        // Arrange - Hospital
        var hospital1 = await GetHospital();
        var hospital2 = await GetHospital();
        var hospitalToPut = new HospitalViewModelFake(id: hospital1.Id, name: hospital2.Name).Generate();

        // Act - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        var exception = await Put<AppException>(_hospitalClient, hospitalToPut);

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Act - Query
        var query = await Query<List<HospitalViewModel>>(_hospitalClient, new HospitalFilter(id: hospital1.Id));
        var hospitalQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
        Assert.Equivalent(hospital1, hospitalQueried);
    }
}
