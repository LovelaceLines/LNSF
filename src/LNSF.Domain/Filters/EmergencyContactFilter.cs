using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class EmergencyContactFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? PeopleId { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public EmergencyContactFilter() { }

    public EmergencyContactFilter(int? id = null,
        string? name = null,
        string? phone = null,
        int? peopleId = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Name = name;
        Phone = phone;
        PeopleId = peopleId;
        Page = page ?? new Pagination();
        OrderBy = orderBy;
    }
}
