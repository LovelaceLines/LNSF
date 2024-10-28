namespace LNSF.Domain.Entities;

public class PatientTreatment : BaseEntity
{
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public int TreatmentId { get; set; }
    public Treatment? Treatment { get; set; }
}
