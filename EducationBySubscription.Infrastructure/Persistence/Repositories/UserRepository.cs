using EducationSubscription.Core.Domain.Users;
using EducationSubscription.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence.Repositories;

/// <inheriteddoc/>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    /// <inheriteddoc/>
    public async Task Add(User entity)
    {
        await _appDbContext.Users.AddAsync(entity);
    }

    /// <inheriteddoc/>
    public async Task<User?> ReadById(Guid id)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(o => o.Id == id);
    }

    /// <inheriteddoc/>
    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _appDbContext.Users.AnyAsync(o => o.Email == email);
    }

    /// <inheriteddoc/>
    public async Task<User?> MatchEmailAndPassword(string email, string password)
    {
        return await _appDbContext.Users.Where(o => o.Email == email && o.Password == password).SingleOrDefaultAsync();
    }

    /// <inheriteddoc/>
    public async Task<User?> ReadByEmail(string email)
    {
        return await _appDbContext.Users.Where(o => o.Email == email).SingleOrDefaultAsync();
    }
}