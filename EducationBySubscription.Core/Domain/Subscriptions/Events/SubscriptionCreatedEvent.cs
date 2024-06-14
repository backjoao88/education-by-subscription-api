using EducationSubscription.Core.Primitives.Contracts;

namespace EducationSubscription.Core.Domain.Subscriptions.Events;

public class SubscriptionCreatedEvent : IDomainEvent
{
    public SubscriptionCreatedEvent(Guid idSubscription, Guid idMember, Guid idPlan)
    {
        IdSubscription = idSubscription;
        IdMember = idMember;
        IdPlan = idPlan;
    }
    public Guid IdSubscription { get; set; }
    public Guid IdMember { get; set; }
    public Guid IdPlan { get; set; }
}