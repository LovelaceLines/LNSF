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

    public UserFilter() { }

    public UserFilter(
        string? id = null,
        string? userName = null,
        string? email = null,
        string? phoneNumber = null,
        string? role = null,
        string? globalFilter = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
        GlobalFilter = globalFilter;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
