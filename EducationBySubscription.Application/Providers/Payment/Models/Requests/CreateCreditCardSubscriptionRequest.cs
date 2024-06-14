namespace EducationBySubscription.Application.Providers.Payment.Models.Requests;

public record CreateCreditCardSubscriptionRequest
{
    public CreateCreditCardSubscriptionRequest(string providerCustomerId, decimal value)
    {
        ProviderCustomerId = providerCustomerId;
        Value = value;
    }
    public string ProviderCustomerId { get; private set; } 
    public decimal Value { get; private set; }
}