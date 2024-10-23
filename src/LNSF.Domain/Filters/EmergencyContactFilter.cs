using AutoFilterer.Attributes;

namespace LNSF.Domain.Filters;

public class EmergencyContactFilter : BaseFilter
{
	public int? Id { get; set; }

	[ToLowerContainsComparison]
	public string? Name { get; set; }

	[ToLowerContainsComparison]
	public string? Phone { get; set; }
	public int? PeopleId { get; set; }
}
