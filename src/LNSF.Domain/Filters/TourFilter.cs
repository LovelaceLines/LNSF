using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class TourFilter
{
    public int? Id { get; set; }
    public DateTime? Output { get; set; }
    public DateTime? Input { get; set; }
    public string? Note { get; set; }
    public bool? InOpen { get; set; }

    public int? PeopleId { get; set; }
    public Pagination Page { get; set; } = new Pagination();
    public OrderBy Order { get; set; } = OrderBy.Ascending;
}
