using LNSF.Api.ViewModels;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestApi : GlobalClientRequest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Post_HostingValidWithEscortsAndChecks_Ok(int escortsCount)
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake1);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalPosted.Id;
        patientFake.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);

        var escortsIds = new List<int>();
        for (int i = 0; i < escortsCount; i++)
        {
            // Arrange - People
            var peopleFake2 = new PeoplePostViewModelFake().Generate();
            var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

            // Arrange - Escort
            var escortFake = new EscortPostViewModelFake().Generate();
            escortFake.PeopleId = peoplePosted2.Id;
            var escortPosted = await Post<EscortViewModel>(_escortClient, escortFake);
            escortsIds.Add(escortPosted.Id);
        }

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.PatientId = patientPosted.Id;
        hostingFake.EscortInfos = new List<HostingEscortInfo>();
        foreach (var escortId in escortsIds)
            hostingFake.EscortInfos.Add(new HostingEscortInfo
            {
                Id = escortId,
                CheckIn = hostingFake.CheckIn,
                CheckOut = null
            });

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Post_HostingValidWithEscortsAndCheckIn_Ok(int escortsCount)
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake1);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalPosted.Id;
        patientFake.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);

        var escortsIds = new List<int>();
        for (int i = 0; i < escortsCount; i++)
        {
            // Arrange - People
            var peopleFake2 = new PeoplePostViewModelFake().Generate();
            var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

            // Arrange - Escort
            var escortFake = new EscortPostViewModelFake().Generate();
            escortFake.PeopleId = peoplePosted2.Id;
            var escortPosted = await Post<EscortViewModel>(_escortClient, escortFake);
            escortsIds.Add(escortPosted.Id);
        }

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.CheckIn = new Bogus.DataSets.Date().Future();
        hostingFake.CheckOut = null;
        hostingFake.PatientId = patientPosted.Id;
        hostingFake.EscortInfos = new List<HostingEscortInfo>();
        foreach (var escortId in escortsIds)
            hostingFake.EscortInfos.Add(new HostingEscortInfo
            {
                Id = escortId,
                CheckIn = hostingFake.CheckIn,
                CheckOut = null
            });

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
    }

    [Fact]
    public async Task Post_HostingValidWithoutEscorts_Ok()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake1);

        // Arrange - Hospital
        var hospitalFake = new HospitalPostViewModelFake().Generate();
        var hospitalPosted = await Post<HospitalViewModel>(_hospitalClient, hospitalFake);

        // Arrange - Treatment
        var treatmentFake = new TreatmentPostViewModelFake().Generate();
        var treatmentPosted = await Post<TreatmentViewModel>(_treatmentClient, treatmentFake);

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalPosted.Id;
        patientFake.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);

        // Arrange - Escorts
        var escortInfos = new List<HostingEscortInfo>();

        // Arrange - Hosting
        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.PatientId = patientPosted.Id;
        hostingFake.EscortInfos = escortInfos;

        // Arrange - Count
        var countBefore = await GetCount(_hostingClient);

        // Act - Hosting
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);

        // Act - Count
        var countAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
    }

    [Fact]
    public async Task Put_HostingValidWithEscortsAndCheckIn_Ok()
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
        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = peoplePosted.Id;
        patientFake.HospitalId = hospitalPosted.Id;
        patientFake.TreatmentIds = new List<int> { treatmentPosted.Id };
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);

        // Arrange - Escorts
        var escortInfos = new List<HostingEscortInfo>();

        // Arrange - Hosting
        var hostingFake1 = new HostingPostViewModelFake().Generate();
        hostingFake1.PatientId = patientPosted.Id;
        hostingFake1.EscortInfos = escortInfos;
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake1);

        // Arrange - Hosting
        var hostingFake2 = new HostingPostViewModelFake().Generate();
        var hostingUpdate = new HostingViewModel
        {
            Id = hostingPosted.Id,
            CheckIn = hostingFake2.CheckIn,
            CheckOut = hostingFake2.CheckOut,
            PatientId = patientPosted.Id,
            EscortInfos = escortInfos
        };

        // Act - Hosting
        var hostingUpdated = await Put<HostingViewModel>(_hostingClient, hostingUpdate);

        // Assert
        Assert.Equivalent(hostingUpdate, hostingUpdated);
    }
}
