using AutoFilterer.Types;

namespace LNSF.Domain.Filters;

public class PeopleRoomHostingFilter : BaseFilter
{
	public int? HostingId { get; set; }
	public int? PeopleId { get; set; }
	public int? RoomId { get; set; }
	public Range<DateTime>? CheckIn { get; set; }
	public Range<DateTime>? CheckOut { get; set; }
	public bool? Active { get; set; }
}
