using AutoFilterer.Attributes;
using AutoFilterer.Types;

namespace LNSF.Domain.Filters;

public class NotificationFilter : BaseFilter
{
	public int? Id { get; set; }
	[ToLowerContainsComparison]
	public string? Title { get; set; }
	public string? Content { get; set; }
	public Range<DateTime>? ValidFrom { get; set; }
	public Range<DateTime>? ExpiredAt { get; set; }
}
