using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class AccountFilter
{
    public string? UserName { get; set; }
    public Role? Role { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}
