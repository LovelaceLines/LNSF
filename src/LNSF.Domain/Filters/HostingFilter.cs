using LNSF.Domain.Enums;
namespace LNSF.Domain.Filters;

public class HostingFilter 
{
  public int? Id { get; set; }
  public DateTime? CheckIn { get; set; }
  public DateTime? CheckOut { get; set; }
  public int? PatientId { get; set; }

}