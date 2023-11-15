using LNSF.Domain.Enums;

namespace LNSF.Domain.Entities;

public class Treatment
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public TypeTreatment Type { get; set; }
} 
