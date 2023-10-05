namespace LNSF.UI.ViewModels;

public class AccountViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Role { get; set; } = "";
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
    public string Role { get; set; } = "";
}

public class AccountPutViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string Role { get; set; } = "";
}

public class AccountDeleteViewModel
{
    public int AccountId { get; set; }
}
