using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HospitalFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Acronym { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public HospitalFilter() { }

    public HospitalFilter(int? id = null,
        string? name = null,
        string? acronym = null,
        string? globalFilter = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Name = name;
        Acronym = acronym;
        GlobalFilter = globalFilter;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
