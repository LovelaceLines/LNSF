using AutoFilterer.Attributes;
using AutoFilterer.Types;

namespace LNSF.Domain.Filters;

public class LogEntryFilter : BaseFilter
{
	public int? Id { get; set; }
	public int? UserId { get; set; }

	[ToLowerContainsComparison]
	public string? EntityName { get; set; }
	public int? EntityId { get; set; }

	[ToLowerContainsComparison]
	public string? Action { get; set; }

	[ToLowerContainsComparison]
	public string? ValuesChanges { get; set; }
	public Range<DateTime>? LogDateTime { get; set; }
}
