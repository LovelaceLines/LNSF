using LNSF.Domain.Enums;

namespace LNSF.Api.ViewModels;

public class PeopleViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public RaceColor RaceColor { get; set; }
    public string Email { get; set; } = "";
    public string RG { get; set; } = "";
    public string IssuingBody { get; set; } = "";
    public string CPF { get; set; } = "";
    public string Street { get; set; } = "";
    public string HouseNumber { get; set; } = "";
    public string Neighborhood { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Note { get; set; } = "";

    public string? Experience { get; set; } = null;
    public string? Status { get; set; } = null;
    public List<TourViewModel>? Tours { get; set; } = [];
    public List<EmergencyContactViewModel>? EmergencyContacts { get; set; } = [];
}

public class PeoplePutViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public RaceColor RaceColor { get; set; }
    public string Email { get; set; } = "";
    public string RG { get; set; } = "";
    public string IssuingBody { get; set; } = "";
    public string CPF { get; set; } = "";
    public string Street { get; set; } = "";
    public string HouseNumber { get; set; } = "";
    public string Neighborhood { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Note { get; set; } = "";
}

public class PeoplePostViewModel
{
    public string Name { get; set; } = "";
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public RaceColor RaceColor { get; set; }
    public string Email { get; set; } = "";
    public string RG { get; set; } = "";
    public string IssuingBody { get; set; } = "";
    public string CPF { get; set; } = "";
    public string Street { get; set; } = "";
    public string HouseNumber { get; set; } = "";
    public string Neighborhood { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Note { get; set; } = "";
}
