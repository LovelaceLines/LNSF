namespace LNSF.UI.ViewModels;

public class AuthenticationTokenViewModel
{
    public int Id { get; set; }
    public string Token { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    
    public int AccountId { get; set; }
}
