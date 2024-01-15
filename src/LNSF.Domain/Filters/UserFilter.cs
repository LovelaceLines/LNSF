using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class UserFilter
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }
}
