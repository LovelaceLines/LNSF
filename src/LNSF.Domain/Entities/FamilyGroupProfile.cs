namespace LNSF.Domain.Entities;

public class FamilyGroupProfile : BaseEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public required string Name { get; set; }
    public required string Kinship { get; set; }
    public int Age { get; set; }
    public required string Profession { get; set; }
    public double Income { get; set; }
}
