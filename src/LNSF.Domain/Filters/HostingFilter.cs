using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HostingFilter 
{
	public int? Id { get; set; }
	public DateTime? PatientCheckIn { get; set; }
	public DateTime? PatientCheckOut { get; set; }
	public DateTime? EscortCheckIn { get; set; }
	public DateTime? EscortCheckOut { get; set; }
	public int? PatientId { get; set; }
	public bool? Active { get; set; }
	public OrderBy OrderBy { get; set; } = OrderBy.Ascending;
	public Pagination Page { get; set; } = new();
}