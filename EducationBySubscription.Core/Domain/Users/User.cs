using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Users;

public class User : Entity
{
    public User(string email, string password, EUserRole userRole)
    {
        Email = email;
        Password = password;
        UserRole = userRole;
    }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public EUserRole UserRole { get; private set; }
}