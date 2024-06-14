using EducationSubscription.Core.Domain.Users;

namespace EducationBySubscription.Application.Providers.Authentication;

public interface IJwtProvider
{
    /// <summary>
    /// Generates a token for authentication use-case.
    /// </summary>
    /// <returns></returns>
    public string GenerateAuthenticationToken(Guid userId, EUserRole userRole);
    /// <summary>
    /// Encrypts a string.
    /// </summary>
    /// <returns></returns>
    public string Encrypt(string str);
}