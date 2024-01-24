using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HospitalTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_Hospital_Ok()
    {
        var hospital = await GetHospital();
        var hospitalFake = new HospitalViewModelFake(id: hospital.Id).Generate();

        var countBefore = await GetCount(_hospitalClient);
        var hospitalPuted = await Put<HospitalViewModel>(_hospitalClient, hospitalFake);
        var countAfter = await GetCount(_hospitalClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(hospitalPuted, hospitalFake);
    }

    [Fact]
    public async Task Put_HospitalInvalidRepeatedUniqueName_Conflict()
    {
        var hospital1 = await GetHospital();
        var hospital2 = await GetHospital();
        var hospitalFake = new HospitalViewModelFake(id: hospital1.Id, name: hospital2.Name).Generate();

        var countBefore = await GetCount(_hospitalClient);
        var exception = await Put<AppException>(_hospitalClient, hospitalFake);
        var countAfter = await GetCount(_hospitalClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
