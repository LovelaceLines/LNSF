using Bogus.Extensions.Brazil;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidPeopleId_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();

        // Assert
        Assert.Equivalent(people.Id, peopleIdQueried.Id);
    }

    [Fact]
    public async Task Get_ValidPeopleNameQuery_Ok()
    {
        // Arrange - People
        var name = new Bogus.DataSets.Name().FullName();
        var people = await GetPeople(name: name);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdName = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, name: name));
        var peopleIdNameQueried = queryIdName.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdNameQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleGenderQuery_Ok()
    {
        // Arrange - People
        var gender = new Bogus.Faker().PickRandom<Gender>();
        var people = await GetPeople(gender: gender);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdGender = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, gender: gender));
        var peopleIdGenderQueried = queryIdGender.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdGenderQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleBirthDateQuery_Ok()
    {
        // Arrange - People
        var birthDate = DateTime.Now.AddYears(-new Bogus.Faker().Random.Int(15, 130));
        var people = await GetPeople(birthDate: birthDate);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdBirthDate = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, birthDate: birthDate));
        var peopleIdBirthDateQueried = queryIdBirthDate.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdBirthDateQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleRGQuery_Ok()
    {
        // Arrange - People
        var rg = new Bogus.Faker().Random.ReplaceNumbers("##.###.###-#");
        var people = await GetPeople(rg: rg);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdRG = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, rg: rg));
        var peopleIdRGQueried = queryIdRG.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdRGQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleIssuingBodyQuery_Ok()
    {
        // Arrange - People
        var issuingBody = new Bogus.Faker().Random.Replace("?????").ToUpper() + "-" + new Bogus.Faker().Random.Replace("??").ToUpper();
        var people = await GetPeople(issuingBody: issuingBody);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdIssuingBody = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, issuingBody: issuingBody));
        var peopleIdIssuingBodyQueried = queryIdIssuingBody.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdIssuingBodyQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleCPFQuery_Ok()
    {
        // Arrange - People
        var cpf = new Bogus.Faker().Person.Cpf();
        var people = await GetPeople(cpf: cpf);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdCPF = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, cpf: cpf));
        var peopleIdCPFQueried = queryIdCPF.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdCPFQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleStreetQuery_Ok()
    {
        // Arrange - People
        var street = new Bogus.Faker().Address.StreetName();
        var people = await GetPeople(street: street);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdStreet = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, street: street));
        var peopleIdStreetQueried = queryIdStreet.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdStreetQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleHouseNumberQuery_Ok()
    {
        // Arrange - People
        var houseNumber = new Bogus.Faker().Address.BuildingNumber();
        var people = await GetPeople(houseNumber: houseNumber);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdHouseNumber = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, houseNumber: houseNumber));
        var peopleIdHouseNumberQueried = queryIdHouseNumber.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdHouseNumberQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleNeighborhoodQuery_Ok()
    {
        // Arrange - People
        var neighborhood = new Bogus.Faker().Address.SecondaryAddress();
        var people = await GetPeople(neighborhood: neighborhood);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdNeighborhood = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, neighborhood: neighborhood));
        var peopleIdNeighborhoodQueried = queryIdNeighborhood.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdNeighborhoodQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleCityQuery_Ok()
    {
        // Arrange - People
        var city = new Bogus.Faker().Address.City();
        var people = await GetPeople(city: city);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdCity = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, city: city));
        var peopleIdCityQueried = queryIdCity.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdCityQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleStateQuery_Ok()
    {
        // Arrange - People
        var state = new Bogus.Faker().Address.StateAbbr();
        var people = await GetPeople(state: state);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, state: state));
        var peopleIdQueried = queryId.First();
        var queryIdState = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, state: state));
        var peopleIdStateQueried = queryIdState.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdStateQueried);
    }

    [Fact]
    public async Task Get_ValidPeoplePhoneQuery_Ok()
    {
        // Arrange - People
        var phone = new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) #####-####");
        var people = await GetPeople(phone: phone);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, phone: phone));
        var peopleIdQueried = queryId.First();
        var queryIdPhone = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, phone: phone));
        var peopleIdPhoneQueried = queryIdPhone.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdPhoneQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleNoteQuery_Ok()
    {
        // Arrange - People
        var note = new Bogus.Faker().Lorem.Sentence();
        var people = await GetPeople(note: note);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, note: note));
        var peopleIdQueried = queryId.First();
        var queryIdNote = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, note: note));
        var peopleIdNoteQueried = queryIdNote.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdNoteQueried);
    }

    [Fact]
    public async Task Get_ValidPatientQuery_Ok()
    {
        // Arrange - People
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdPatient = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, patient: true));
        var peopleIdPatientQueried = queryIdPatient.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdPatientQueried);
    }

    [Fact]
    public async Task Get_ValidEscortQuery_Ok()
    {
        // Arrange - People
        var people = await GetPeople();
        var escort = await GetEscort(peopleId: people.Id);

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdEscort = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, escort: true));
        var peopleIdEscortQueried = queryIdEscort.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdEscortQueried);
    }

    [Fact]
    public async Task Get_ValidActiveQuery_Ok()
    {
        // Arrange - People
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdActive = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, active: true));
        var peopleIdActiveQueried = queryIdActive.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdActiveQueried);
    }

    [Fact]
    public async Task Get_ValidVeteranQuery_Ok()
    {
        // Arrange - People
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting1 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-5));
        var hosting2 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleIdQueried = queryId.First();
        var queryIdVeteran = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, veteran: true));
        var peopleIdVeteranQueried = queryIdVeteran.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, peopleIdVeteranQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleGlobalQuery_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Act - Query
        var queryId = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));

        var peopleIdQueried = queryId.First();

        var queryIdName = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Name));
        var queryIdRG = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.RG));
        var queryIdIssuingBody = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.IssuingBody));
        var queryIdCPF = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.CPF));
        var queryIdPhone = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Phone));
        var queryIdStreet = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Street));
        var queryIdHouseNumber = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.HouseNumber));
        var queryIdNeighborhood = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Neighborhood));
        var queryIdCity = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.City));
        var queryIdState = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.State));
        var queryIdNote = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Note));

        var PeopleIdName = queryIdName.First();
        var PeopleIdRG = queryIdRG.First();
        var PeopleIdIssuingBody = queryIdIssuingBody.First();
        var PeopleIdCPF = queryIdCPF.First();
        var PeopleIdPhone = queryIdPhone.First();
        var PeopleIdStreet = queryIdStreet.First();
        var PeopleIdHouseNumber = queryIdHouseNumber.First();
        var PeopleIdNeighborhood = queryIdNeighborhood.First();
        var PeopleIdCity = queryIdCity.First();
        var PeopleIdState = queryIdState.First();
        var PeopleIdNote = queryIdNote.First();

        // Assert
        Assert.Equivalent(peopleIdQueried, PeopleIdName);
        Assert.Equivalent(peopleIdQueried, PeopleIdRG);
        Assert.Equivalent(peopleIdQueried, PeopleIdIssuingBody);
        Assert.Equivalent(peopleIdQueried, PeopleIdCPF);
        Assert.Equivalent(peopleIdQueried, PeopleIdPhone);
        Assert.Equivalent(peopleIdQueried, PeopleIdStreet);
        Assert.Equivalent(peopleIdQueried, PeopleIdHouseNumber);
        Assert.Equivalent(peopleIdQueried, PeopleIdNeighborhood);
        Assert.Equivalent(peopleIdQueried, PeopleIdCity);
        Assert.Equivalent(peopleIdQueried, PeopleIdState);
        Assert.Equivalent(peopleIdQueried, PeopleIdNote);
    }
}
