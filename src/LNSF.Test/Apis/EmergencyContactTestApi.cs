using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Newtonsoft.Json;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidContact_OK()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(contactFake, contactPosted);
    }

    [Fact]
    public async Task Post_InvalidContactWithEmptyName_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.Name = "";
        contactFake.PeopleId = peoplePosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        await Assert.ThrowsAsync<Exception>(async () => await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidContactWithEmptyPhone_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.Phone = "";
        contactFake.PeopleId = peoplePosted.Id;

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        await Assert.ThrowsAsync<Exception>(async () => await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidContactWithInvalidPeopleId_BadRequest()
    {
        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = -1;

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        await Assert.ThrowsAsync<Exception>(async () => await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
    
    [Fact]
    public async Task Put_ValidContact_OK()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var otherContactFake = new EmergencyContactViewModelFake().Generate();
        var contactMapped = _mapper.Map<EmergencyContactViewModel>(otherContactFake);
        contactMapped.Id = contactPosted.Id;
        contactMapped.PeopleId = peoplePosted.Id;
        var contactPuted = await Put<EmergencyContactViewModel>(_emergencyContactClient, contactMapped);
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(contactMapped, contactPuted);
    }

    [Fact]
    public async Task Put_InvalidContactWithEmptyName_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var contactMapped = _mapper.Map<EmergencyContactViewModel>(contactFake);
        contactMapped.Name = "";
        contactMapped.Id = contactPosted.Id;
        contactMapped.PeopleId = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(async () => await Put<EmergencyContactViewModel>(_emergencyContactClient, contactMapped));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidContactWithEmptyPhone_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var contactMapped = _mapper.Map<EmergencyContactViewModel>(contactFake);
        contactMapped.Phone = "";
        contactMapped.Id = contactPosted.Id;
        contactMapped.PeopleId = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(async () => await Put<EmergencyContactViewModel>(_emergencyContactClient, contactMapped));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidContactWithInvalidPeopleId_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var contactMapped = _mapper.Map<EmergencyContactViewModel>(contactFake);
        contactMapped.Id = contactPosted.Id;
        contactMapped.PeopleId = -1;
        await Assert.ThrowsAsync<Exception>(async () => await Put<EmergencyContactViewModel>(_emergencyContactClient, contactMapped));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidContactWithOtherPeopleId_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);
        var otherPeopleFake = new PeoplePostViewModelFake().Generate();
        var otherpeoplePosted = await Post<PeopleViewModel>(_peopleClient, otherPeopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var otherContactFake = new EmergencyContactViewModelFake().Generate();
        var contactMapped = _mapper.Map<EmergencyContactViewModel>(otherContactFake);
        contactMapped.Id = contactPosted.Id;
        contactMapped.PeopleId = otherpeoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(async () => await Put<EmergencyContactViewModel>(_emergencyContactClient, contactMapped));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Delete_ValidContact_OK()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);
 
        // Act
        var contactDeleted = await Delete<EmergencyContactViewModel>(_emergencyContactClient, contactPosted.Id);
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(contactFake, contactDeleted);
    }

    [Fact]
    public async Task Delete_InvalidContact_BadRequest()
    {
        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);
 
        // Act
        // Contact with invalid Id
        await Assert.ThrowsAsync<Exception>(async () => await Delete<EmergencyContactViewModel>(_emergencyContactClient, -1));
        await Assert.ThrowsAsync<Exception>(async () => await Delete<EmergencyContactViewModel>(_emergencyContactClient, 0));
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

}