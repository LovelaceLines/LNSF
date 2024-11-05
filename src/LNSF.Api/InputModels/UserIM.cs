using LNSF.Domain.Entities;

namespace LNSF.API.InputModels;

public class UserAndPassword : User
{
	public string Password { get; set; } = null!;
}

public class UpdatePasswordIM
{
	public string OldPassword { get; set; } = string.Empty;
	public string NewPassword { get; set; } = string.Empty;
}
