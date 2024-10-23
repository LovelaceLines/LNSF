using AutoFilterer.Attributes;
using AutoFilterer.Types;

namespace LNSF.Domain.Filters;

public class TourFilter : BaseFilter
{
	public int? Id { get; set; }
	public Range<DateTime>? Output { get; set; }
	public Range<DateTime>? Input { get; set; }

	[ToLowerContainsComparison]
	public string? Note { get; set; }
	public int? PeopleId { get; set; }
}
