namespace LNSF.Domain.Entities;

public class FamilyGroupProfile
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public string Name { get; set; } = "";
    public string Kinship { get; set; } = "";
    public int Age { get; set; }
    public string Profession { get; set; } = "";
    public double Income { get; set; }
}
