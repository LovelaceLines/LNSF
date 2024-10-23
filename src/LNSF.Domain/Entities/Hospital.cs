namespace LNSF.Domain.Entities;

public class Hospital
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Acronym { get; set; }
}
