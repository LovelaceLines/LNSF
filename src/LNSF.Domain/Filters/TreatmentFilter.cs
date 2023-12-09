using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class TreatmentFilter 
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public TypeTreatment? Type { get; set; }
    
    public Pagination Page { get; set; } = new();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}