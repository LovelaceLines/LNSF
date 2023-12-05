using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class RoleFilter
{
    public string? Id { get; set; }
    public string? Name { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}
