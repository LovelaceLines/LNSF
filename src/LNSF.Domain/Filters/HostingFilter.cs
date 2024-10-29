using AutoFilterer.Types;

namespace LNSF.Domain.Filters;

public class HostingFilter : BaseFilter
{
	public int? Id { get; set; }
	public int? PatientId { get; set; }
	public Range<DateTime>? CheckIn { get; set; }
	public Range<DateTime>? CheckOut { get; set; }
}
