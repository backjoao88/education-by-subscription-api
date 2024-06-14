namespace EducationSubscription.Core.Primitives.Errors;

public class UserErrors
{
    public static Error UserEmailAlreadyExists => new("User.EmailAlreadyExists", "This e-mail is already in use."); 
    public static Error UserNotFound => new("User.NotFound", "This user was not found."); 
    public static Error UserInvalidCredentials => new("User.InvalidCredentials", "Invalid credentials."); 
}