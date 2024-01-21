using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HostingEscortFilter
{
    public int? HostingId { get; set; }
    public int? EscortId { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public HostingEscortFilter() { }

    public HostingEscortFilter(int? hostingId = null,
        int? escortId = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        HostingId = hostingId;
        EscortId = escortId;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}
