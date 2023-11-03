using LNSF.Api.ViewModels;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_PatientValid_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalPosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(patientFake, patientPosted);
    }

    [Fact]
    public async Task Post_PatientValidWithRelationshipHospitalValid_Ok()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted1.Id;
        patientFake1.HospitalId = hospitalPosted.Id;

        var patientFake2 = new PatientPostViewModelFake().Generate();
        patientFake2.PeopleId = peoplePosted2.Id;
        patientFake2.HospitalId = hospitalPosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);
        var patientPosted2 = await Post<PatientViewModel>(_patientClient, patientFake2);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(patientFake1, patientPosted1);
        Assert.Equivalent(patientFake2, patientPosted2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Post_PatientInvalidWithPeopleIdInvalid_BadRequest(int peopleId)
    {
        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peopleId;
        patientFake.HospitalId = hospitalPosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        await Assert.ThrowsAsync<Exception>(() => Post<PatientViewModel>(_patientClient, patientFake));

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Post_PatientInvalidWithHospitalIdInvalid_BadRequest(int hospitalId)
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalId;

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        await Assert.ThrowsAsync<Exception>(() => Post<PatientViewModel>(_patientClient, patientFake));

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
    
    [Fact]
    public async Task Post_PatientInvaliWithRelationshipInvalid_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted1.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);

        var patientFake2 = new PatientPostViewModelFake().Generate();
        patientFake2.PeopleId = peoplePosted2.Id;
        patientFake2.HospitalId = hospitalPosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted2 = await Post<PatientViewModel>(_patientClient, patientFake2);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(patientFake2, patientPosted2);
    }

    [Fact]
    public async Task Put_PatientValid_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);

        var patientFake2 = new PatientPostViewModelFake().Generate();
        var patientPut = new PatientViewModel
        {
            Id = patientPosted1.Id,
            PeopleId = patientPosted1.PeopleId,
            HospitalId = patientPosted1.HospitalId,
            Term = patientFake2.Term,
            SocioeconomicRecord = patientFake2.SocioeconomicRecord
        };

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted2 = await Put<PatientViewModel>(_patientClient, patientPut);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(patientPut, patientPosted2);
    }

    [Fact]	
    public async Task Put_PatientInvalidWithRelationshipPeopleInvalid_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted1.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);

        var patientFake2 = new PatientPostViewModelFake().Generate();
        patientFake2.PeopleId = peoplePosted1.Id;
        patientFake2.HospitalId = hospitalPosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        await Assert.ThrowsAsync<Exception>(() => Put<PatientViewModel>(_patientClient, patientFake2));

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
}
