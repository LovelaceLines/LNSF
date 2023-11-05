namespace LNSF.Domain.Entities;

public class Hosting 
{ 
	public int Id { get; set; }
	public DateTime CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }
	public int PatientId { get; set; }
	public Patient? Patient { get; set; }
	public List<int> EscortsIds { get; set; } = new();
}