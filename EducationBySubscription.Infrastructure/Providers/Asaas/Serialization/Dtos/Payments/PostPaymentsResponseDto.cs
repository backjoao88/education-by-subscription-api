using EducationBySubscription.Application.Providers.Payment.Serialization;
using Newtonsoft.Json;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Serialization.Dtos.Payments;

public record PostPaymentsResponseDto : PaymentDto
{
    [JsonProperty("id")] 
    public string Id { get; set; } = string.Empty;
    [JsonProperty("bankSlipUrl")]
    public string Link { get; set; } = string.Empty;
}