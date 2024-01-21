using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class HostingFilter
{
	public int? Id { get; set; }
	public DateTime? CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }
	public int? PatientId { get; set; }
	public bool? GetPatient { get; set; }
	public int? EscortId { get; set; }
	public bool? GetEscort { get; set; }
	public string? GlobalFilter { get; set; }
	public bool? Active { get; set; }

	public Pagination Page { get; set; } = new();
	public OrderBy? OrderBy { get; set; }

	public HostingFilter() { }

	public HostingFilter(int? id = null,
		int? patientId = null,
		int? escortId = null,
		DateTime? checkIn = null,
		DateTime? checkOut = null,
		bool? active = null,
		Pagination? page = null,
		OrderBy? orderBy = null)
	{
		Id = id;
		CheckIn = checkIn;
		CheckOut = checkOut;
		PatientId = patientId;
		EscortId = escortId;
		Active = active;
		Page = page ?? Page;
		OrderBy = orderBy;
	}
}