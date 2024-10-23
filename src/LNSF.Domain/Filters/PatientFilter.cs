namespace LNSF.Domain.Filters;

public class PatientFilter : BaseFilter
{
	public int? Id { get; set; }
	public int? PeopleId { get; set; }
	public int? HospitalId { get; set; }
	public bool? SocioEconomicRecord { get; set; }
	public bool? Term { get; set; }
	public int? TreatmentId { get; set; }
	public bool? Active { get; set; }
	public bool? IsVeteran { get; set; }
}
