namespace LNSF.Domain.DTOs;

public class TourFilters
{
    public int? Id { get; set; }
    public DateTime? Output { get; set; }
    public DateTime? Input { get; set; } = DateTime.MaxValue;
    public string? Note { get; set; }

    public int? PeopleId { get; set; }
    public Pagination Page { get; set; } = new Pagination();
    public OrderBy Order { get; set; } = OrderBy.Ascending;
}
