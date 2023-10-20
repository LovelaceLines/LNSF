using LNSF.Domain.Enums;

namespace LNSF.Api.ViewModels;

public class AccountViewModel
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public Role Role { get; set; }
}

public class AccountLoginViewModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}

public class AccountPostViewModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public Role Role { get; set; }
}

public class AccountPutViewModel
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public Role Role { get; set; }
}

public class AccountPutPasswordViewModel
{
    public string Id { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";
}

public class AccountDeleteViewModel
{
    public string AccountId { get; set; } = "";
}
