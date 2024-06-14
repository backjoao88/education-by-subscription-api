using EducationSubscription.Core.Domain.Subscriptions.Enumerations;

namespace EducationBySubscription.Application.Core.Subscriptions.Views;

public record SubscriptionViewModel(
    Guid Id,
    Guid IdPlan,
    Guid? IdMember,
    ESubscriptionStatus Status
);