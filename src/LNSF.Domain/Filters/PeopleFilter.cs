using AutoFilterer.Attributes;
using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PeopleFilter : BaseFilter
{
	public int? Id { get; set; }

	[ToLowerContainsComparison]
	public string? Name { get; set; }

	[ToLowerContainsComparison]
	public string? RG { get; set; }

	[ToLowerContainsComparison]
	public string? IssuingBody { get; set; }

	[ToLowerContainsComparison]
	public string? CPF { get; set; }

	[ToLowerContainsComparison]
	public string? Phone { get; set; }
	public Gender? Gender { get; set; }
	public DateTime? BirthDate { get; set; }

	[ToLowerContainsComparison]
	public string? Street { get; set; }

	[ToLowerContainsComparison]
	public string? HouseNumber { get; set; }

	[ToLowerContainsComparison]
	public string? Neighborhood { get; set; }

	[ToLowerContainsComparison]
	public string? City { get; set; }

	[ToLowerContainsComparison]
	public string? State { get; set; }

	[ToLowerContainsComparison]
	public string? Note { get; set; }

	public bool? IsPatient { get; set; }
	public bool? IsEscort { get; set; }
	public bool? IsActive { get; set; }
	public bool? IsVeteran { get; set; }
}
