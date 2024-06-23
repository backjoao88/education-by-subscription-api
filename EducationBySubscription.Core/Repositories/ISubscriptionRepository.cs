using EducationSubscription.Core.Domain.Subscriptions;
using EducationSubscription.Core.Repositories.Contracts;

namespace EducationSubscription.Core.Repositories;

public interface ISubscriptionRepository : IWritableRepository<Subscription>, IReadableAllRepository<Subscription>,
    IReadableRepository<Subscription>, IDeletableRepository
{
    public Task<List<Subscription>> ReadActiveByMember(Guid memberId);
    public Task<List<Guid>> ReadCoursesByActiveMember(Guid memberId);
}