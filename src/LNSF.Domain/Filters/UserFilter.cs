using AutoFilterer.Attributes;

namespace LNSF.Domain.Filters;

public class UserFilter : BaseFilter
{
	public int? Id { get; set; }

	[ToLowerContainsComparison]
	public string? Name { get; set; }

	[ToLowerContainsComparison]
	public string? UserName { get; set; }

	[ToLowerContainsComparison]
	public string? Email { get; set; }

	[ToLowerContainsComparison]
	public string? PhoneNumber { get; set; }
	public string? Roles { get; set; }
}
