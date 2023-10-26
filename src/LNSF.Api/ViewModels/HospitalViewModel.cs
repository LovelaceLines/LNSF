namespace LNSF.Api.ViewModels;

public class HospitalViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Acronym { get; set; }
}

public class HospitalPostViewModel
{
    public string Name { get; set; } = "";
    public string? Acronym { get; set; }
}
