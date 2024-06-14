using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Serialization.Dtos.Payments;

public class PostConfirmPaymentDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("payment")] 
    public Payment Payment { get; set; } = null!;
}

public class Payment
{
    [JsonPropertyName("id")] 
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("subscription")] 
    public string ExternalSubscriptionId { get; set; }= string.Empty;
    [JsonPropertyName("value")] 
    public decimal Value { get; set; } = 0;
}