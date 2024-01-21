using LNSF.Domain.Entities;

namespace LNSF.Domain.DTOs;

public class HostingDTO : Hosting
{
    public List<Escort>? Escorts { get; set; } = null;
}
