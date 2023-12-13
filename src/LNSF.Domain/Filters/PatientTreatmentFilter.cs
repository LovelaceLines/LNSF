namespace LNSF.Domain.Filters;

public class PatientTreatmentFilter
{
    public int? PatientId { get; set; }
    public int? TreatmentId { get; set; }

    public Pagination Page { get; set; } = new();

    public PatientTreatmentFilter() { }

    public PatientTreatmentFilter(int? patientId = null, 
        int? treatmentId = null, 
        Pagination? page = null)
    {
        PatientId = patientId;
        TreatmentId = treatmentId;
        Page = page ?? new();
    }
}
