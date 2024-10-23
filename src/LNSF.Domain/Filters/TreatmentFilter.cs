using AutoFilterer.Attributes;
using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class TreatmentFilter : BaseFilter
{
	public int? Id { get; set; }

	[ToLowerContainsComparison]
	public string? Name { get; set; }
	public TypeTreatment? Type { get; set; }
}
