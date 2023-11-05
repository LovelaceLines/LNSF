﻿using LNSF.Api.ViewModels;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApi : GlobalClientRequest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Post_PatientValid_Ok(int numberOfTreatments)
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Treatment
        var treatments = new List<int>();
        foreach (var _ in Enumerable.Range(0, numberOfTreatments))
        {
            var treatmentFake = new TreatmentPostViewModelFake().Generate();
            var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);
            treatments.Add(treatmentPosted.Id);
        }
        
        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalPosted.Id;
        patientFake.TreatmentIds = treatments;

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
    public async Task Post_PatientValidWithRelationshipHospitalAndTreatmentValid_Ok()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted1.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        patientFake1.TreatmentIds = new List<int> { treatmentPosted.Id };

        var patientFake2 = new PatientPostViewModelFake().Generate();
        patientFake2.PeopleId = peoplePosted2.Id;
        patientFake2.HospitalId = hospitalPosted.Id;
        patientFake2.TreatmentIds = new List<int> { treatmentPosted.Id };

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

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peopleId;
        patientFake.HospitalId = hospitalPosted.Id;
        patientFake.TreatmentIds = new List<int> { treatmentPosted.Id };

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

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalId;
        patientFake.TreatmentIds = new List<int> { treatmentPosted.Id };

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

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted1.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        patientFake1.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);

        var patientFake2 = new PatientPostViewModelFake().Generate();
        patientFake2.PeopleId = peoplePosted2.Id;
        patientFake2.HospitalId = hospitalPosted.Id;
        patientFake2.TreatmentIds = new List<int> { treatmentPosted.Id };

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

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        patientFake1.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);

        var patientFake2 = new PatientPostViewModelFake().Generate();
        var patientPut = new PatientViewModel
        {
            Id = patientPosted1.Id,
            PeopleId = patientPosted1.PeopleId,
            HospitalId = patientPosted1.HospitalId,
            Term = patientFake2.Term,
            SocioeconomicRecord = patientFake2.SocioeconomicRecord,
            TreatmentIds = new List<int> { treatmentPosted.Id }
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

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake().Generate();
        patientFake1.PeopleId = peoplePosted1.Id;
        patientFake1.HospitalId = hospitalPosted.Id;
        patientFake1.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);

        var patientFake2 = new PatientPostViewModelFake().Generate();
        patientFake2.PeopleId = peoplePosted1.Id;
        patientFake2.HospitalId = hospitalPosted.Id;
        patientFake2.TreatmentIds = new List<int> { treatmentPosted.Id };

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
