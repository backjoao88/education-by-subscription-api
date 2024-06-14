using EducationBySubscription.Application.Core.Users.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Users.Commands.Login;

/// <summary>
/// Represents a login command.
/// </summary>
public class LoginUserCommand : IRequest<Result<LoginViewModel>>
{
    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
    public string Email { get; set; }
    public string Password { get; set; }
}