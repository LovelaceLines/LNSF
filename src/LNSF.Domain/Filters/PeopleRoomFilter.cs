using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PeopleRoomFilter
{
    public int? Id { get; set; }
    public int? Occupation { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public int? PeopleId { get; set; }
    public int? RoomId { get; set; }
    public int? HostingId { get; set; }

    public Pagination Page { get; set; } = new Pagination();
    public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
}
