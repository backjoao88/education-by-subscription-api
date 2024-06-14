namespace EducationBySubscription.Application.Core.Users.Views;

/// <summary>
/// Represents a login view model.
/// </summary>
public class LoginViewModel
{
    public LoginViewModel(string token)
    {
        Token = token;
    }
    public string Token { get; set; }
}