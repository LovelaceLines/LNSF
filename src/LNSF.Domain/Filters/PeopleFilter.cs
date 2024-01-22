using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PeopleFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? RG { get; set; }
    public string? IssuingBody { get; set; }
    public string? CPF { get; set; }
    public string? Phone { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Note { get; set; }
    public bool? Patient { get; set; }
    public bool? Escort { get; set; }
    public bool? Active { get; set; }
    public bool? Veteran { get; set; }
    public string? GlobalFilter { get; set; }
    public bool? GetTours { get; set; }
    public bool? GetEmergencyContacts { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public PeopleFilter() { }

    public PeopleFilter(int? id = null,
        string? name = null,
        string? rg = null,
        string? issuingBody = null,
        string? cpf = null,
        string? phone = null,
        Gender? gender = null,
        DateTime? birthDate = null,
        string? street = null,
        string? houseNumber = null,
        string? neighborhood = null,
        string? city = null,
        string? state = null,
        string? note = null,
        bool? patient = null,
        bool? escort = null,
        bool? active = null,
        bool? veteran = null,
        string? globalFilter = null,
        bool? getTours = null,
        bool? getEmergencyContacts = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Name = name;
        RG = rg;
        IssuingBody = issuingBody;
        CPF = cpf;
        Phone = phone;
        Gender = gender;
        BirthDate = birthDate;
        Street = street;
        HouseNumber = houseNumber;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Note = note;
        Patient = patient;
        Escort = escort;
        Active = active;
        Veteran = veteran;
        GlobalFilter = globalFilter;
        GetTours = getTours;
        GetEmergencyContacts = getEmergencyContacts;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
