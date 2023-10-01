namespace LNSF.Domain.DTOs;

public class EmergencyContactFilters
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? PeopleId { get; set; }

    public Pagination Page { get; set; } = new();
}
