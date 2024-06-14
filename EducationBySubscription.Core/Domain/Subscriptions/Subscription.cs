using System.Globalization;
using EducationSubscription.Core.Domain.Members;
using EducationSubscription.Core.Domain.Plans;
using EducationSubscription.Core.Domain.Subscriptions.Enumerations;
using EducationSubscription.Core.Domain.Subscriptions.Events;
using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Subscriptions;

public class Subscription : Entity
{
    private Subscription(ESubscriptionStatus status, Guid idPlan, Guid idMember)
    {
        Status = status;
        IdPlan = idPlan;
        IdMember = idMember;
    }

    /// <summary>
    /// Creates a new subscription
    /// </summary>
    /// <param name="idPlan"></param>
    /// <param name="idMember"></param>
    /// <returns></returns>
    public static Subscription Create(Guid idPlan, Guid idMember)
    {
        DateTime today = DateTime.Now;
        DateOnly start = new DateOnly(today.Year, today.Month, today.Day);
        DateOnly end = new DateOnly(today.Year, today.Month, today.Day);
        Subscription subscription = new Subscription(ESubscriptionStatus.Deactivated, idPlan, idMember);
        subscription.Raise(new SubscriptionCreatedEvent(subscription.Id, idMember, idPlan));
        return subscription;
    }
    
    public Guid IdPlan { get; private set; }
    public Plan Plan { get; private set; } = null!;
    public Guid IdMember { get; private set; }
    public Member Member { get; private set; } = null!;
    public ESubscriptionStatus Status { get; private set; }
}