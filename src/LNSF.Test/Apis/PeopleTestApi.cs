using Bogus.Extensions.Brazil;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApi : GlobalClientRequest
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

    [Fact]
    public async Task Post_ValidPeople_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Act - Query
        var query = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: peoplePosted.Id));
        var peopleQueried = query.First();

        peopleQueried.Experience = null;
        peopleQueried.Status = null;

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
        Assert.Equivalent(peoplePosted, peopleQueried);
    }

    [Fact]
    public async Task Post_validPeopleWithPhoneNumber_OK()
    {
        // Arrange - People
        var acceptPeople1 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) #####-####"));
        var acceptPeople2 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("####-####"));

        var peopleNotAccept1 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) # ####-####"));
        var peopleNotAccept2 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) ####-####"));
        var peopleNotAccept6 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("## (##) ####-####"));
        var peopleNotAccept7 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("## (##) # ####-####"));

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, acceptPeople1.Generate());
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, acceptPeople2.Generate());

        var exception1 = await Post<AppException>(_peopleClient, peopleNotAccept1.Generate());
        var exception2 = await Post<AppException>(_peopleClient, peopleNotAccept2.Generate());
        var exception6 = await Post<AppException>(_peopleClient, peopleNotAccept6.Generate());
        var exception7 = await Post<AppException>(_peopleClient, peopleNotAccept7.Generate());

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception1.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception2.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception6.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception7.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidPeople_BadRequest()
    {
        // Arrange - People
        var peopleWithoutName = new PeoplePostViewModelFake(name: "").Generate();
        var peopleAged14 = new PeoplePostViewModelFake(birthDate: DateTime.Now.AddYears(-14)).Generate();
        var peopleAged129 = new PeoplePostViewModelFake(birthDate: DateTime.Now.AddYears(-129)).Generate();
        var peopleWithInvalidRG = new PeoplePostViewModelFake(rg: "123456789").Generate();
        var peopleWithInvalidCPF = new PeoplePostViewModelFake(cpf: "123456789").Generate();
        var peopleWithInvalidPhone = new PeoplePostViewModelFake(phone: "123456789").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var exceptionWithoutName = await Post<AppException>(_peopleClient, peopleWithoutName);
        var exceptionAged14 = await Post<AppException>(_peopleClient, peopleAged14);
        var exceptionAged129 = await Post<AppException>(_peopleClient, peopleAged129);
        var exceptionWithInvalidRG = await Post<AppException>(_peopleClient, peopleWithInvalidRG);
        var exceptionWithInvalidCPF = await Post<AppException>(_peopleClient, peopleWithInvalidCPF);
        var exceptionWithInvalidPhone = await Post<AppException>(_peopleClient, peopleWithInvalidPhone);

        // Arrange - Count
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidRG.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidCPF.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidPhone.StatusCode);
    }

    [Fact]
    public async Task Put_ValidPeople_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        var peopleToPut = new PeopleViewModelFake(people.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var peoplePuted = await Put<PeopleViewModel>(_peopleClient, peopleToPut);

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Act - Query
        var query = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleQueried = query.First();

        peopleQueried.Experience = null;
        peopleQueried.Status = null;

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(peopleToPut, peoplePuted);
        Assert.Equivalent(peoplePuted, peopleQueried);
    }

    [Fact]
    public async Task Put_InvalidPeople_BadRequest()
    {
        // Arrange - People
        var people = await GetPeople();

        var peopleToPutWithoutName = new PeopleViewModelFake(people.Id, name: "").Generate();
        var peopleToPutAged14 = new PeopleViewModelFake(people.Id, birthDate: DateTime.Now.AddYears(-14)).Generate();
        var peopleToPutAged129 = new PeopleViewModelFake(people.Id, birthDate: DateTime.Now.AddYears(-129)).Generate();
        var peopleToPutWithInvalidRG = new PeopleViewModelFake(people.Id, rg: "123456789").Generate();
        var peopleToPutWithInvalidCPF = new PeopleViewModelFake(people.Id, cpf: "123456789").Generate();
        var peopleToPutWithInvalidPhone = new PeopleViewModelFake(people.Id, phone: "123456789").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var exceptionWithoutName = await Put<AppException>(_peopleClient, peopleToPutWithoutName);
        var exceptionAged14 = await Put<AppException>(_peopleClient, peopleToPutAged14);
        var exceptionAged129 = await Put<AppException>(_peopleClient, peopleToPutAged129);
        var exceptionWithInvalidRG = await Put<AppException>(_peopleClient, peopleToPutWithInvalidRG);
        var exceptionWithInvalidCPF = await Put<AppException>(_peopleClient, peopleToPutWithInvalidCPF);
        var exceptionWithInvalidPhone = await Put<AppException>(_peopleClient, peopleToPutWithInvalidPhone);

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidRG.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidCPF.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidPhone.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientHotingRoom_OK()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 1, available: true);

        // Arrange - People
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleEscortHotingRoomWithEscort_OK()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 1, available: true);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleEscortHotingRoomWithPatientAndEscort_Ok()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 2, available: true);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore + 2, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFakeWithPatient, peopleRoomPostedWithPatient);
        Assert.Equivalent(peopleRoomFakeWithEscort, peopleRoomPostedWithEscort);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientActiveHostingRoom_OK()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 1, available: true);

        // Arrange - People
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientActiveHostingRoomWithEscort_OK()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 2, available: true);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore + 2, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFakeWithPatient, peopleRoomPostedWithPatient);
        Assert.Equivalent(peopleRoomFakeWithEscort, peopleRoomPostedWithEscort);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidHosting_NotFound()
    {
        // Arrange - Room
        var room = await GetRoom(available: true);

        // Arrange - People
        var patient = await GetPatient();

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: 0).Generate();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidRoom_NotFound()
    {
        // Arrange - People
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: 0, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidPeople_BadRequest()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: true);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: 0, hostingId: hostingFake.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithValidHostingPatientButInvalidPeople_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom(available: true);

        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithValidHostingPatientAndEscortButInvalidPeople_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom(available: true);

        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithRoomNotAvailable_Conflict()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: false);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithoutVacancyToPatientAndEscort_Conflict()
    {
        // Arrange - Room
        var room = await GetRoom(available: true, beds: 1);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomWithPatient);

        var peopleRoomWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomWithEscort);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomWithPatient, peopleRoomPostedWithPatient);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleHostingRoomWithPatient_OK()
    {
        // Arrange - PeopleRoom
        var peopleRoomFake = await GetPeopleRoom();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var peopleRoomRemoved = await DeleteByBody<PeopleRoomViewModel>(_removePeopleFromRoom, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore - 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomRemoved);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleHostingRoomWithEscort_OK()
    {
        // Arrange - People
        var escort = await GetEscort();
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);

        // Arrange - Room
        var room = await GetRoom(available: true, beds: 2);

        // Arrange - PeopleRoom
        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(peopleId: patient.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);

        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(peopleId: escort.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var peopleRoomRemovedWithEscort = await DeleteByBody<PeopleRoomViewModel>(_removePeopleFromRoom, peopleRoomFakeWithEscort);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore - 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFakeWithEscort, peopleRoomRemovedWithEscort);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_InvalidPeopleHostingRoomWithNotExistEntity_BadRequest()
    {
        // Arrange - PeopleRoom
        var peopleRoom = await GetPeopleRoom();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var exceptionRoom = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = 0, PeopleId = peopleRoom.PeopleId, HostingId = peopleRoom.HostingId });
        var exceptionPeople = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = peopleRoom.RoomId, PeopleId = 0, HostingId = peopleRoom.HostingId });
        var exceptionHosting = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = peopleRoom.RoomId, PeopleId = peopleRoom.PeopleId, HostingId = 0 });

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.NotEqual(HttpStatusCode.OK, exceptionRoom.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionPeople.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionHosting.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionRoom.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionPeople.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionHosting.StatusCode);
    }
}
