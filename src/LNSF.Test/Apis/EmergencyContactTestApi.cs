/*using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Newtonsoft.Json;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApi
{
    private readonly GlobalClientRequest _global = new();
    private readonly IMapper _mapper;
    private readonly PeopleTestApi _peopleTestApi = new();

    public EmergencyContactTestApi()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmergencyContactPostViewModel, EmergencyContactViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Post_ValidContact_OK()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await _peopleTestApi.Post(peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;

        // Arrange - Quantity
        var quantityContactsBefore = await GetQuantity();

        // Act
        var contactPosted = await Post(contactFake);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityContactsBefore + 1, quantityAfter);
        Assert.Equivalent(contactFake, contactPosted);
    }

    [Fact]
    public async Task Post_InvalidContact_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await _peopleTestApi.Post(peopleFake1);

        // Arrange - Contact
        var contactFake1 = new EmergencyContactViewModelFake().Generate();
        contactFake1.Name = "";
        contactFake1.PeopleId = peoplePosted1.Id;
        var contactFake2 = new EmergencyContactViewModelFake().Generate();
        contactFake2.Phone = "";
        contactFake2.PeopleId = peoplePosted1.Id;
        var contactFake3 = new EmergencyContactViewModelFake().Generate();
        contactFake3.PeopleId = -1;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // Contact with invalid name
        await Assert.ThrowsAsync<Exception>(async () => await Post(contactFake1));
        // Contact with invalid phone
        await Assert.ThrowsAsync<Exception>(async () => await Post(contactFake2));
        // Contact with invalid PeopleId
        await Assert.ThrowsAsync<Exception>(async () => await Post(contactFake3));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }
    
    [Fact]
    public async Task Put_ValidContact_OK()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await _peopleTestApi.Post(peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post(contactFake);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var contactFake2 = new EmergencyContactViewModelFake().Generate();
        var contactMap = _mapper.Map<EmergencyContactViewModel>(contactFake2);
        contactMap.Id = contactPosted.Id;
        contactMap.PeopleId = peoplePosted.Id;
        var contactPuted = await Put(contactMap);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
        Assert.Equivalent(contactMap, contactPuted);
    }
    
    [Fact]
    public async Task Put_InvalidContact_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await _peopleTestApi.Post(peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await _peopleTestApi.Post(peopleFake2);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted1.Id;
        var contactPosted = await Post(contactFake);

        var contactFake1 = new EmergencyContactViewModelFake().Generate();
        var contactMap1 = _mapper.Map<EmergencyContactViewModel>(contactFake1);
        contactMap1.Name = "";
        contactMap1.Id = contactPosted.Id;
        contactMap1.PeopleId = peoplePosted1.Id;

        var contactFake2 = new EmergencyContactViewModelFake().Generate();
        var contactMap2 = _mapper.Map<EmergencyContactViewModel>(contactFake2);
        contactMap2.Phone = "";
        contactMap2.Id = contactPosted.Id;
        contactMap2.PeopleId = peoplePosted1.Id;

        var contactFake3 = new EmergencyContactViewModelFake().Generate();
        var contactMap3 = _mapper.Map<EmergencyContactViewModel>(contactFake3);
        contactMap3.Id = contactPosted.Id;
        contactMap3.PeopleId = -1;

        var contactFake4 = new EmergencyContactViewModelFake().Generate();
        var contactMap4 = _mapper.Map<EmergencyContactViewModel>(contactFake4);
        contactMap4.Id = contactPosted.Id;
        contactMap4.PeopleId = peoplePosted2.Id;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // Contact with invalid name
        await Assert.ThrowsAsync<Exception>(async () => await Put(contactMap1));
        // Contact with invalid phone
        await Assert.ThrowsAsync<Exception>(async () => await Put(contactMap2));
        // Contact with invalid PeopleId
        await Assert.ThrowsAsync<Exception>(async () => await Put(contactMap3));
        // Contact with non-existent PeopleId
        await Assert.ThrowsAsync<Exception>(async () => await Put(contactMap4));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Delete_ValidContact_OK()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await _peopleTestApi.Post(peopleFake);

        // Arrange - Contact
        var contactFake = new EmergencyContactViewModelFake().Generate();
        contactFake.PeopleId = peoplePosted.Id;
        var contactPosted = await Post(contactFake);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var contactDeleted = await Delete(contactPosted.Id);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore - 1, quantityAfter);
        Assert.Equivalent(contactFake, contactDeleted);
    }

    [Fact]
    public async Task Delete_InvalidContact_BadRequest()
    {
        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // Contact with invalid Id
        await Assert.ThrowsAsync<Exception>(async () => await Delete(-1));
        await Assert.ThrowsAsync<Exception>(async () => await Delete(0));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    public async Task<EmergencyContactViewModel> Get(int id)
    {
        var response = await _global._emergencyContactClient.GetAsync($"{id}");
        var content = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<EmergencyContactViewModel>>(content);
        var value = values?.FirstOrDefault();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<int> GetQuantity()
    {
        var response = await _global._emergencyContactClient.GetAsync("quantity");
        var content = await response.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<int>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value;
    }

    public async Task<EmergencyContactViewModel> Post(EmergencyContactPostViewModel contact)
    {
        var contactJson = JsonContent.Create(contact);
        var response = await _global._emergencyContactClient.PostAsync("", contactJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<EmergencyContactViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }
    
    public async Task<EmergencyContactViewModel> Put(EmergencyContactViewModel contact)
    {
        var contactJson = JsonContent.Create(contact);
        var response = await _global._emergencyContactClient.PutAsync("", contactJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<EmergencyContactViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<EmergencyContactViewModel> Delete(int id)
    {
        var response = await _global._emergencyContactClient.DeleteAsync($"{id}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<EmergencyContactViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }
}
*/