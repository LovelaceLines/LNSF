using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HostingFilter 
{
	public int? Id { get; set; }
	public DateTime? CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }
	public int? PatientId { get; set; }
	public int? EscortId { get; set; }
	public bool? Active { get; set; }

	public Pagination Page { get; set; } = new();
	public OrderBy? OrderBy { get; set; }
}