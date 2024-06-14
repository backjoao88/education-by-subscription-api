using EducationSubscription.Core.Domain.Subscriptions;
using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Payments;

public class Payment : Entity
{
    private Payment( string externalIdSubscription, decimal value)
    {
        Value = value;
        ExternalIdSubscription = externalIdSubscription;
    }
    
    public string ExternalIdSubscription { get; private set; }
    public decimal Value { get; private set; }

    public static Payment Create(string externalIdSubscription, decimal value)
    {
        var payment = new Payment(externalIdSubscription, value);
        return payment;
    }
}