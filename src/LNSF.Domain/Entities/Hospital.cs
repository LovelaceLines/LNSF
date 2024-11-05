namespace LNSF.Domain.Entities;

public class Hospital : BaseEntity
{
	public required string Name { get; set; }
	public required string Acronym { get; set; }
}
