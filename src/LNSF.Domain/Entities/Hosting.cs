namespace LNSF.Domain.Entities;

public class Hosting : BaseEntity
{
	public DateTime CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }

	public int PatientId { get; set; }
	public Patient? Patient { get; set; }
}
