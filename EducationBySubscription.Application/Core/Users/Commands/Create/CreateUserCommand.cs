using EducationBySubscription.Application.Core.Users.Views;
using EducationSubscription.Core.Domain.Users;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Users.Commands.Create;

/// <summary>
/// Represents the command to create a user.
/// </summary>
public class CreateUserCommand : IRequest<Result<UserCreatedViewModel>>
{
    public CreateUserCommand(string email, string password, EUserRole role)
    {
        Email = email;
        Password = password;
        Role = role;
    }
    public string Email { get; set; }
    public string Password { get; set; }
    public EUserRole Role { get; set; }
}