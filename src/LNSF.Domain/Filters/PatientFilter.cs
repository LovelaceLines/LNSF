using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PatientFilter 
{
    public int? Id { get; set; }
    public int? PatientId { get; set; }
    public int? HospitalId { get; set; }
    public bool? SocioEconomicRecord { get; set; }
    public bool? Term { get; set; }
    public int? TreatmentId { get; set; }
    
    public Pagination Page { get; set; } = new();
    public OrderBy Order { get; set; } = OrderBy.Ascending;
}