using EducationBySubscription.Application.Providers.Payment.Models.Requests;
using EducationBySubscription.Application.Providers.Payment.Models.Responses;

namespace EducationBySubscription.Application.Providers.Payment;

public interface IPaymentProvider
{
    public Task<CreateSubscriptionResponse?> CreateCreditCardSubscription(CreateCreditCardSubscriptionRequest createCreditCardSubscriptionRequest);
    public Task<RetrieveCustomerProviderResponse?> GetCustomerByDocumentNumber(string documentNumber);
}