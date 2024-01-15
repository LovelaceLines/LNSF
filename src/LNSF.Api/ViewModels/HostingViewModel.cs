namespace LNSF.Api.ViewModels;

public class HostingViewModel
{ 
	public int Id { get; set; }
	public DateTime CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }
	public int PatientId { get; set; }
	public PatientViewModel? Patient { get; set; } = null;
	public List<EscortViewModel>? Escorts { get; set; } = null;
}

public class HostingPostViewModel
{ 
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public int PatientId { get; set; }
}