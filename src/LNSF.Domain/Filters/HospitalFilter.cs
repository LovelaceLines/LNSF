using AutoFilterer.Attributes;

namespace LNSF.Domain.Filters;

public class HospitalFilter : BaseFilter
{
	public int? Id { get; set; }

	[ToLowerContainsComparison]
	public string? Name { get; set; }

	[ToLowerContainsComparison]
	public string? Acronym { get; set; }
}
