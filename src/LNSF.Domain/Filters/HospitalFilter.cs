using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HospitalFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Acronym { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }
}
