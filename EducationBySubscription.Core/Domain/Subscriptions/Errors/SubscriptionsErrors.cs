using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Subscriptions.Errors;

public static class SubscriptionsErrors
{ 
    public static Error SubscriptionNotFound = new("Subscription.NotFound", "The subscription was not found."); 
}