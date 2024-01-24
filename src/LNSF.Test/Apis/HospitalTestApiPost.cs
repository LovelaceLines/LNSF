using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HospitalTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task Post_HospitalValid_Ok()
    {
        var hospitalFake = new HospitalPostViewModelFake().Generate();

        var countBefore = await GetCount(_hospitalClient);
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);
        var countAfter = await GetCount(_hospitalClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hospitalFake, hospitalPosted);
    }

    [Fact]
    public async Task Post_HospitalWithInvalidName_BadRequest()
    {
        var hospitalWithInvalidName = new HospitalPostViewModelFake(name: "hosp").Generate();

        var countBefore = await GetCount(_hospitalClient);
        var exceptionWithInvalidName = await Post<AppException>(_hospitalClient, hospitalWithInvalidName);
        var countAfter = await GetCount(_hospitalClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidName.StatusCode);
    }

    [Fact]
    public async Task Post_HospitalWithRepeatedUniqueName_Conflict()
    {
        var hospital = await GetHospital();
        var hospitalFake = new HospitalPostViewModelFake(name: hospital.Name).Generate();

        var countBefore = await GetCount(_hospitalClient);
        var exception = await Post<AppException>(_hospitalClient, hospitalFake);
        var countAfter = await GetCount(_hospitalClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
