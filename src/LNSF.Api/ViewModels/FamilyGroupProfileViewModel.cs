namespace LNSF.Api.ViewModels;

public class FamilyGroupProfileViewModel
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public PatientViewModel? Patient { get; set; }
    public string Name { get; set; } = "";
    public string Kinship { get; set; } = "";
    public int Age { get; set; }
    public string Profession { get; set; } = "";
    public double Income { get; set; }
}

public class FamilyGroupProfilePostViewModel
{
    public int PatientId { get; set; }
    public PatientViewModel? Patient { get; set; }
    public string Name { get; set; } = "";
    public string Kinship { get; set; } = "";
    public int Age { get; set; }
    public string Profession { get; set; } = "";
    public double Income { get; set; }
}
