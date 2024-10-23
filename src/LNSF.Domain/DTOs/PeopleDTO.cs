using LNSF.Domain.Entities;

namespace LNSF.Domain.DTOs;

public class PeopleDTO : People
{
	public string? Experience { get; set; }
	public string? Status { get; set; }
	public List<Tour> Tours { get; set; } = [];
	public List<EmergencyContact> EmergencyContacts { get; set; } = [];
}
