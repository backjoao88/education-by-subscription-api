using System.Net;
using EducationBySubscription.Application.Providers.Payment;
using EducationBySubscription.Application.Providers.Payment.Models.Enums;
using EducationBySubscription.Application.Providers.Payment.Models.Requests;
using EducationBySubscription.Application.Providers.Payment.Models.Responses;
using EducationBySubscription.Application.Providers.Payment.Serialization;
using EducationBySubscription.Infrastructure.Providers.Asaas.Clients;
using EducationBySubscription.Infrastructure.Providers.Asaas.Contracts;
using EducationBySubscription.Infrastructure.Providers.Asaas.Serialization.Dtos.Customers;
using EducationBySubscription.Infrastructure.Providers.Asaas.Serialization.Dtos.Payments;
using Microsoft.Extensions.Logging;

namespace EducationBySubscription.Infrastructure.Providers.Asaas;

public class AsaasProvider : IPaymentProvider
{
    private readonly IDefaultHttpClient _httpClient;
    private readonly IDefaultHttpSerializer _serializer;
    private readonly ILogger<AsaasProvider> _logger;

    public AsaasProvider(IDefaultHttpClient httpClient, IDefaultHttpSerializer serializer, ILogger<AsaasProvider> logger)
    {
        _httpClient = httpClient;
        _serializer = serializer;
        _logger = logger;
    }
    
    public async Task<CreateSubscriptionResponse?> CreateCreditCardSubscription(CreateCreditCardSubscriptionRequest createCreditCardSubscriptionRequest)
    {
        var defaultNextDueDate = GetDefaultNextDueDate();
        var chargeTypesMap = new Dictionary<EProviderChargeType, string>()
        {
            { EProviderChargeType.CreditCard, "CREDIT_CARD" },
            { EProviderChargeType.Pix, "PIX" }
        };
        
        // Create the request body
        var requestBody = new PostSubscriptionDto(
            createCreditCardSubscriptionRequest.ProviderCustomerId,
            chargeTypesMap[EProviderChargeType.Pix],
            (double) createCreditCardSubscriptionRequest.Value,
            defaultNextDueDate
        );
        
        // Serialize request
        var paymentRequest = _serializer.Serialize(requestBody);
        
        // Send the request
        var httpResponse = await _httpClient.Post(AsaasResources.SubscriptionEndpoint, new StringContent(paymentRequest));
        if (httpResponse.StatusCode != HttpStatusCode.OK) return default!;
        var paymentResponseJson = await httpResponse.Content.ReadAsStringAsync();
        if (!paymentResponseJson.Any()) return default!;
        
        // Desserialize
        var paymentResponse = _serializer.Deserialize<PostPaymentsResponseDto>(paymentResponseJson);
        if (paymentResponse is null) return default!;
        
        _logger.LogDebug("Payment: {0}", paymentResponse.Id);
        
        return new CreateSubscriptionResponse(paymentResponse.Id, paymentResponse.Link);
    }

    public async Task<RetrieveCustomerProviderResponse?> GetCustomerByDocumentNumber(string documentNumber)
    {
        if (!documentNumber.Any()) return default!;
        
        // Build the request parameters
        var requestUri = $"{AsaasResources.CustomerEndpoint}?cpfCnpj={documentNumber}";
        
        // Send request
        var httpResponse = await _httpClient.GetAsync(requestUri);
        if (httpResponse.StatusCode != HttpStatusCode.OK) return null!;
        var customerResponseJson = await httpResponse.Content.ReadAsStringAsync();
        if (!customerResponseJson.Any()) return default!;
        
        // Desserialize
        var customerResponse = _serializer.Deserialize<GetCustomersResponseDto>(customerResponseJson);
        if (customerResponse is null) return default!;
        if (customerResponse.Customers.Count == 0) return default!;
        var foundCustomer = customerResponse.Customers.First();
        
        _logger.LogDebug("Customer: {0} - {1}", foundCustomer.Id, foundCustomer.Name);
        
        return new RetrieveCustomerProviderResponse(foundCustomer.Id, foundCustomer.Name, "", "");
    }

    public DateTime GetDefaultNextDueDate()
    {
        var today = DateTime.Now;
        var defaultBillingDay = 15;
        var nextDueDate = new DateTime(today.Year, today.Month, defaultBillingDay);
        nextDueDate = today.Day > defaultBillingDay ? nextDueDate.AddMonths(1) : nextDueDate;
        if (today.Day > defaultBillingDay)
        {
            nextDueDate = nextDueDate.AddMonths(1);
        }
        return nextDueDate;
    }
    
}