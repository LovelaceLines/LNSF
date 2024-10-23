using AutoFilterer.Attributes;

namespace LNSF.Domain.Filters;

public class FamilyGroupProfileFilter : BaseFilter
{
	public int? Id { get; set; }
	public int? PatientId { get; set; }

	[ToLowerContainsComparison]
	public string? Name { get; set; }
}
