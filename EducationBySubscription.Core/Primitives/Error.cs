namespace EducationSubscription.Core.Primitives;

/// <summary>
/// Represents a generic base error.
/// </summary>
public record Error 
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public static Error None => new Error(string.Empty, string.Empty);
    public string Code { get; set; }
    public string Message { get; set; }
}