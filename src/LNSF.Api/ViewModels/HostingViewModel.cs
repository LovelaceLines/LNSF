using LNSF.Domain.DTOs;

namespace LNSF.Api.ViewModels;

public class HostingViewModel
{ 
	public int Id { get; set; }
	public DateTime CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }
	public List<EscortHostingInfo>? EscortInfos { get; set; }
	public int PatientId { get; set; }
}

public class HostingPostViewModel
{ 
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public List<EscortHostingInfo>? EscortInfos { get; set; }
    public int PatientId { get; set; }
}