using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.Entities;

public class UserRole : IdentityUserRole<int>
{
	public int Id { get; set; }
	override public int UserId { get; set; }
	public User? User { get; set; }
	override public int RoleId { get; set; }
	public Role? Role { get; set; }
}
