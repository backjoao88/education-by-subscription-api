using EducationSubscription.Core.Domain.Subscriptions;
using EducationSubscription.Core.Domain.Subscriptions.Enumerations;
using EducationSubscription.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _appDbContext;

    public SubscriptionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(Subscription member)
    {
        await _appDbContext.Subscriptions.AddAsync(member);
    }

    public Task<List<Subscription>> ReadAll()
    {
        return _appDbContext.Subscriptions.ToListAsync();
    }

    public Task<Subscription?> ReadById(Guid id)
    {
        return _appDbContext.Subscriptions.SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var subscription = await ReadById(id);
        if (subscription is null) return false;
        _appDbContext.Subscriptions.Remove(subscription);
        return true;
    }

    public Task<List<Subscription>> ReadActiveByMember(Guid memberId)
    {
        return _appDbContext
            .Subscriptions
            .Where(o => o.Status == ESubscriptionStatus.Active && o.IdMember == memberId)
            .Include(o => o.Plan)
            .ToListAsync();
    }

    public async Task<List<Guid>> ReadCoursesByActiveMember(Guid memberId)
    {
        var member = await ReadActiveByMember(memberId);
        return member
            .Select(o => o.Plan)
            .SelectMany(o => o.AllowedCourses)
            .Distinct()
            .ToList();
    }
}