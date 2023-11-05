namespace LNSF.Api.ViewModels;

public class PatientViewModel 
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public int HospitalId { get; set; }
    public bool SocioeconomicRecord { get; set; }
    public bool Term { get; set; }
    public List<int> TreatmentIds { get; set; } = new();
}

public class PatientPostViewModel 
{
    public int PeopleId { get; set; }
    public int HospitalId { get; set; }
    public bool SocioeconomicRecord { get; set; }
    public bool Term { get; set; }
    public List<int> TreatmentIds { get; set; } = new();
}