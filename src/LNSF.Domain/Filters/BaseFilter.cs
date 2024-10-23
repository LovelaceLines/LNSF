using AutoFilterer.Types;
using System.ComponentModel;

namespace LNSF.Domain.Filters;

public class BaseFilter : PaginationFilterBase
{
	[DefaultValue(1)]
	public override int Page { get; set; } = 1;

	[DefaultValue(20)]
	public override int PerPage { get; set; } = 20;
}
