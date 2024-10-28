using LNSF.Domain.Enums;

namespace LNSF.Domain.Entities;

public class People : BaseEntity
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public Gender Gender { get; set; }
	public DateOnly BirthDate { get; set; }
	public MaritalStatus MaritalStatus { get; set; }
	public RaceColor RaceColor { get; set; }
	public string? Email { get; set; }
	public required string RG { get; set; }
	public required string IssuingBody { get; set; }
	public required string CPF { get; set; }
	public required string Street { get; set; }
	public required string HouseNumber { get; set; }
	public required string Neighborhood { get; set; }
	public required string City { get; set; }
	public required string State { get; set; }
	public string? Phone { get; set; }
	public string? Note { get; set; }
}
