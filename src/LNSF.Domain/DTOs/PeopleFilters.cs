namespace LNSF.Domain.DTOs;

public class PeopleFilters
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? Phone { get; set; }
    public Pagination Page { get; set; } = new Pagination();
}
