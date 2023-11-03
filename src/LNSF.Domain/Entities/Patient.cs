namespace LNSF.Domain.Entities;

public class Patient 
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public People? People { get; set; }
    public int HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
    public bool SocioeconomicRecord { get; set; }
    public bool Term { get; set; }
    public List<Treatment> Treatments { get; set; } = new();
}