using EducationSubscription.Core.Domain.Users;
using EducationSubscription.Core.Repositories.Contracts;

namespace EducationSubscription.Core.Repositories;

/// <summary>
/// Contract to a <see cref="User"/> data repository.
/// </summary>
public interface IUserRepository : IWritableRepository<User>, IReadableRepository<User>, IReadableEmailRepository<User>
{
    /// <summary>
    /// Checks the uniqueness of the email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<bool> IsEmailUnique(string email);
    /// <summary>
    /// Checks if email and password matches with one saved.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<User?> MatchEmailAndPassword(string email, string password);
}