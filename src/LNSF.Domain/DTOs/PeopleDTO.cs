using LNSF.Domain.Entities;

namespace LNSF.Domain.DTOs;

public class PeopleDTO : People
{
    public string? Experience { get; set; } = null;
    public string? Status { get; set; } = null;
    public List<Tour>? Tours { get; set; } = null;
    public List<EmergencyContact>? EmergencyContacts { get; set; } = null;
}
