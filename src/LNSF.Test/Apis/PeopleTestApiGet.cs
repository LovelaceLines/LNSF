using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryPeople_Ok()
    {
        var people = await GetPeople();

        var peopleQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id));

        Assert.Equivalent(people.Id, peopleQueried.Id);
        Assert.Equivalent(people.Name, peopleQueried.Name);
        Assert.Equivalent(people.Gender, peopleQueried.Gender);
        Assert.Equivalent(people.BirthDate, peopleQueried.BirthDate);
        Assert.Equivalent(people.RG, peopleQueried.RG);
        Assert.Equivalent(people.IssuingBody, peopleQueried.IssuingBody);
        Assert.Equivalent(people.CPF, peopleQueried.CPF);
        Assert.Equivalent(people.Street, peopleQueried.Street);
        Assert.Equivalent(people.HouseNumber, peopleQueried.HouseNumber);
        Assert.Equivalent(people.Neighborhood, peopleQueried.Neighborhood);
        Assert.Equivalent(people.City, peopleQueried.City);
        Assert.Equivalent(people.State, peopleQueried.State);
        Assert.Equivalent(people.Phone, peopleQueried.Phone);
        Assert.Equivalent(people.Note, peopleQueried.Note);
        Assert.Equivalent("Novato", peopleQueried.Experience);
        Assert.Equivalent("Sem status", peopleQueried.Status);
    }

    [Fact]
    public async Task QueryPeopleName_Ok()
    {
        var people = await GetPeople();

        var peopleNameQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, name: people.Name));

        Assert.Equivalent(people.Name, peopleNameQueried.Name);
    }

    [Fact]
    public async Task QueryPeopleGender_Ok()
    {
        var people = await GetPeople();

        var peopleGenderQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, gender: people.Gender));

        Assert.Equivalent(people.Gender, peopleGenderQueried.Gender);
    }

    [Fact]
    public async Task QueryPeopleBirthDate_Ok()
    {
        var people = await GetPeople();

        var peopleBirthDateQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, birthDate: people.BirthDate));

        Assert.Equivalent(people.BirthDate, peopleBirthDateQueried.BirthDate);
    }

    [Theory]
    [InlineData("##.###.###-#")]
    public async Task QueryPeopleRG_Ok(string rgFormat)
    {
        var rg = new Bogus.Faker().Random.ReplaceNumbers(rgFormat);
        var people = await GetPeople(rg: rg);

        var peopleRGQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, rg: rg));

        Assert.Equivalent(people.RG, peopleRGQueried.RG);
    }

    [Fact]
    public async Task QueryPeopleIssuingBody_Ok()
    {
        var people = await GetPeople();

        var peopleIssuingBodyQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, issuingBody: people.IssuingBody));

        Assert.Equivalent(people.IssuingBody, peopleIssuingBodyQueried.IssuingBody);
    }

    [Fact]
    public async Task QueryPeopleCPF_Ok()
    {
        var people = await GetPeople();

        var peopleCPFQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, cpf: people.CPF));

        Assert.Equivalent(people.CPF, peopleCPFQueried.CPF);
    }

    [Fact]
    public async Task QueryPeopleStreet_Ok()
    {
        var people = await GetPeople();

        var peopleStreetQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, street: people.Street));

        Assert.Equivalent(people.Street, peopleStreetQueried.Street);
    }

    [Fact]
    public async Task QueryPeopleHouseNumber_Ok()
    {
        var people = await GetPeople();

        var peopleHouseNumberQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, houseNumber: people.HouseNumber));

        Assert.Equivalent(people.HouseNumber, peopleHouseNumberQueried.HouseNumber);
    }

    [Fact]
    public async Task QueryPeopleNeighborhood_Ok()
    {
        var people = await GetPeople();

        var peopleNeighborhoodQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, neighborhood: people.Neighborhood));

        Assert.Equivalent(people.Neighborhood, peopleNeighborhoodQueried.Neighborhood);
    }

    [Fact]
    public async Task QueryPeopleCity_Ok()
    {
        var people = await GetPeople();

        var peopleCityQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, city: people.City));

        Assert.Equivalent(people.City, peopleCityQueried.City);
    }

    [Fact]
    public async Task QueryPeopleState_Ok()
    {
        var people = await GetPeople();

        var peopleStateQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, state: people.State));

        Assert.Equivalent(people.State, peopleStateQueried.State);
    }

    [Theory]
    [InlineData("(##) #####-####")]
    [InlineData("####-####")]
    public async Task QueryPeoplePhone_Ok(string phoneFormat)
    {
        var phone = new Bogus.DataSets.PhoneNumbers().PhoneNumber(phoneFormat);
        var people = await GetPeople(phone: phone);

        var peoplePhoneQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, phone: phone));

        Assert.Equivalent(people.Phone, peoplePhoneQueried.Phone);
    }

    [Fact]
    public async Task QueryPeopleNoteQuery_Ok()
    {
        var people = await GetPeople();

        var peopleNoteQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, note: people.Note));

        Assert.Equivalent(people.Note, peopleNoteQueried.Note);
    }

    [Fact]
    public async Task QueryPeoplePatient_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);

        var peoplePatientQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, patient: true));

        Assert.Equivalent(people.Id, peoplePatientQueried.Id);
    }

    [Fact]
    public async Task QueryPeopleEscort_Ok()
    {
        var people = await GetPeople();
        var escort = await GetEscort(peopleId: people.Id);

        var peopleEscortQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, escort: true));

        Assert.Equivalent(people.Id, peopleEscortQueried.Id);
    }

    [Fact]
    public async Task QueryPeopleActive_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        var peopleActiveQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, active: true));

        Assert.Equivalent(people.Id, peopleActiveQueried.Id);
    }

    [Fact]
    public async Task QueryPeopleNew_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        var peopleNewQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, veteran: false));

        Assert.Equivalent(people.Id, peopleNewQueried.Id);
    }

    [Fact]
    public async Task QueryPeopleVeteran_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting1 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-5));
        var hosting2 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        var peopleVeteranQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, veteran: true));

        Assert.Equivalent(people.Id, peopleVeteranQueried.Id);
    }

    [Fact]
    public async Task QueryPeopleGlobalFilter_Ok()
    {
        var people = await GetPeople();
        var peopleQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id));

        var peopleNameQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Name));
        var peopleRGQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.RG));
        var peopleIssuingBodyQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.IssuingBody));
        var peopleCPFQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.CPF));
        var peoplePhoneQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Phone));
        var peopleStreetQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Street));
        var peopleHouseNumberQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.HouseNumber));
        var peopleNeighborhoodQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Neighborhood));
        var peopleCityQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.City));
        var peopleStateQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.State));
        var peopleNoteQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, globalFilter: people.Note));

        Assert.Equivalent(peopleQueried, peopleNameQueried);
        Assert.Equivalent(peopleQueried, peopleRGQueried);
        Assert.Equivalent(peopleQueried, peopleIssuingBodyQueried);
        Assert.Equivalent(peopleQueried, peopleCPFQueried);
        Assert.Equivalent(peopleQueried, peoplePhoneQueried);
        Assert.Equivalent(peopleQueried, peopleStreetQueried);
        Assert.Equivalent(peopleQueried, peopleHouseNumberQueried);
        Assert.Equivalent(peopleQueried, peopleNeighborhoodQueried);
        Assert.Equivalent(peopleQueried, peopleCityQueried);
        Assert.Equivalent(peopleQueried, peopleStateQueried);
        Assert.Equivalent(peopleQueried, peopleNoteQueried);
    }

    [Fact]
    public async Task QueryPeopleGetTours_Ok()
    {
        var people = await GetPeople();
        var openTour1 = await GetTour(peopleId: people.Id);
        var closedTour1 = await GetTour(id: openTour1.Id, peopleId: people.Id);
        var tour2 = await GetTour(peopleId: people.Id);
        people.Tours!.Add(closedTour1);
        people.Tours.Add(tour2);

        var peopleToursQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, getTours: true));

        Assert.Equivalent(people.Tours, peopleToursQueried.Tours);
    }

    [Fact]
    public async Task QueryPeopleGetEmergencyContacts_Ok()
    {
        var people = await GetPeople();
        var emergencyContact1 = await GetEmergencyContact(peopleId: people.Id);
        var emergencyContact2 = await GetEmergencyContact(peopleId: people.Id);
        people.EmergencyContacts!.Add(emergencyContact1);
        people.EmergencyContacts.Add(emergencyContact2);

        var peopleEmergencyContactsQueried = await QuerySingle<PeopleViewModel>(_peopleClient, new PeopleFilter(id: people.Id, getEmergencyContacts: true));

        Assert.Equivalent(people.EmergencyContacts, peopleEmergencyContactsQueried.EmergencyContacts);
    }
}
