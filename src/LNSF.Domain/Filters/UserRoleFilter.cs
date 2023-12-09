using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class UserRoleFilter
{
    public string? UserId { get; set; }
    public string? RoleId { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}
