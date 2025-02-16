namespace LNSF.Domain.Filters;

public class PeopleRoomHostingFilter : BaseFilter
{
	public int? HostingId { get; set; }
	public HostingFilter? Hosting { get; set; }
	public int? PeopleId { get; set; }
	public PeopleFilter? People { get; set; }
	public int? RoomId { get; set; }
	public RoomFilter? Room { get; set; }
	public bool? IsActive { get; set; }
}
