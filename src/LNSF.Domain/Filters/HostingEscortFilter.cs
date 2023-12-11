using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HostingEscortFilter
{
    public int? HostingId { get; set; }
    public int? EscortId { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public HostingEscortFilter() { }

    public HostingEscortFilter(int? hostingId, int? escortId, OrderBy? orderBy, int page = 1)
    {
        HostingId = hostingId;
        EscortId = escortId;
        Page = new(page);
        OrderBy = orderBy;
    }
}
