namespace LNSF.Domain.Filters;

public class HostingEscortFilter
{
    public int? HostingId { get; set; }
    public int? EscortId { get; set; }

    public Pagination Page { get; set; } = new();
}
