using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PeopleFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? Phone { get; set; }
    public bool? Patient { get; set; }
    public bool? Escort { get; set; }
    public bool? Active { get; set; }
    
    public Pagination Page { get; set; } = new Pagination();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}
