using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class EmergencyContactFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? PeopleId { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}
