namespace LNSF.Api.ViewModels;

public class PatientViewModel 
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public PeopleViewModel? People { get; set; } = null;
    public int HospitalId { get; set; }
    public bool SocioeconomicRecord { get; set; }
    public bool Term { get; set; }
}

public class PatientPostViewModel 
{
    public int PeopleId { get; set; }
    public int HospitalId { get; set; }
    public bool SocioeconomicRecord { get; set; }
    public bool Term { get; set; }

}