using LNSF.Api.ViewModels;
using Microsoft.Identity.Client;
using Xunit;

namespace LNSF.Test.Apis;

public class HospitalTestApi : GlobalClientRequest
{
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

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hospitalFake, hospitalPosted);
    }

    [Theory]
    [InlineData("Hosp")]
    public async Task Post_HospitalInvalid_BadRequest(string name)
    {
        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        hospitalFake.Name = name;

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        await Assert.ThrowsAsync<Exception>(() => Post<HospitalViewModel>(_hospitalClient, hospitalFake));

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_HospitalInvalidWithUniqueName_BadRequest()
    {
        // Arrange - Hospital
        var hospitalFake1 = new HospitalPostViewModelFake().Generate();
        var hospitalPosted1 = await Post<HospitalViewModel>(_hospitalClient, hospitalFake1);
        var hospitalFake2 = new HospitalPostViewModelFake().Generate();
        hospitalFake2.Name = hospitalPosted1.Name;

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        await Assert.ThrowsAsync<Exception>(() => Post<HospitalViewModel>(_hospitalClient, hospitalFake2));

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_HospitalValid_Ok()
    {
        // Arrange - Hospital
        var hospitalFake1 = new HospitalPostViewModelFake().Generate();
        var hospitalPosted1 = await Post<HospitalViewModel>(_hospitalClient, hospitalFake1);
        var hospitalFake2 = new HospitalPostViewModelFake().Generate();
        var hospitalPut = new HospitalViewModel
        {
            Id = hospitalPosted1.Id,
            Name = hospitalFake2.Name,
            Acronym = hospitalFake2.Acronym
        };

        // Arrange - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        var hospitalPuted = await Put<HospitalViewModel>(_hospitalClient, hospitalPut);

        // Arrange - Count
        var countAfter = await GetCount(_hospitalClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(hospitalPuted, hospitalPut);
    }

    [Fact]
    public async Task Put_HospitalInvalidWithUniqueName_Ok()
    {
        // Arrange - Hospital
        var hospitalFake1 = new HospitalPostViewModelFake().Generate();
        var hospitalPosted1 = await Post<HospitalViewModel>(_hospitalClient, hospitalFake1);
        var hospitalFake2 = new HospitalPostViewModelFake().Generate();
        var hospitalPosted2 = await Post<HospitalViewModel>(_hospitalClient, hospitalFake2);
        var hospitalPut = new HospitalViewModel
        {
            Id = hospitalPosted1.Id,
            Name = hospitalPosted2.Name,
            Acronym = hospitalPosted2.Acronym
        };

        // Act - Count
        var countBefore = await GetCount(_hospitalClient);

        // Act - Hospital
        await Assert.ThrowsAsync<Exception>(() => Put<HospitalViewModel>(_hospitalClient, hospitalPut));

        // Act - Count
        var countAfter = await GetCount(_hospitalClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

}
