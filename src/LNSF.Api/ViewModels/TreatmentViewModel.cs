using LNSF.Domain.Enums;

namespace LNSF.Api.ViewModels;

public class TreatmentViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public TypeTreatment Type { get; set; }
}

public class TreatmentPostViewModel
{
    public string Name { get; set; } = "";
    public TypeTreatment Type { get; set; }
}
