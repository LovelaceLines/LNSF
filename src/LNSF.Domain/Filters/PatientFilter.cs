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
    public bool? Active { get; set; }
    public bool? IsVeteran { get; set; }
    
    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public PatientFilter() { }

    public PatientFilter(int? id = null,
        int? patientId = null,
        int? hospitalId = null,
        bool? socioEconomicRecord = null,
        bool? term = null,
        int? treatmentId = null,
        bool? active = null,
        bool? isVeteran = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        PatientId = patientId;
        HospitalId = hospitalId;
        SocioEconomicRecord = socioEconomicRecord;
        Term = term;
        TreatmentId = treatmentId;
        Active = active;
        IsVeteran = isVeteran;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}