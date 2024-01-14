using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class TreatmentFilter 
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public TypeTreatment? Type { get; set; }
    public string? GlobalFilter { get; set; }
    
    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public TreatmentFilter() { }

    public TreatmentFilter(int? id = null, 
        string? name = null, 
        TypeTreatment? type = null, 
        Pagination? page = null, 
        OrderBy? orderBy = null)
    {
        Id = id;
        Name = name;
        Type = type;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}