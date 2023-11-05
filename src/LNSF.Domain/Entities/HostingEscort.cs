namespace LNSF.Domain.Entities;

public class HostingEscort
{
    public int HostingId { get; set; }
    public Hosting? Hosting { get; set; }
    public int EscortId { get; set; }
    public Escort? Escort { get; set; }
}
