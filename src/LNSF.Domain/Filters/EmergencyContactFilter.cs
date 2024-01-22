using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class EmergencyContactFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? PeopleId { get; set; }
    public bool? GetPeople { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public EmergencyContactFilter() { }

    public EmergencyContactFilter(
        int? id = null,
        string? name = null,
        string? phone = null,
        int? peopleId = null,
        bool? getPeople = null,
        string? globalFilter = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Name = name;
        Phone = phone;
        PeopleId = peopleId;
        GetPeople = getPeople;
        GlobalFilter = globalFilter;
        Page = page ?? new Pagination();
        OrderBy = orderBy;
    }
}
