using EducationBySubscription.Application.Providers.Payment.Serialization;
using Newtonsoft.Json;

namespace EducationBySubscription.Infrastructure.Providers.Asaas.Serialization.Dtos.Payments;

public record PostSubscriptionDto : PaymentDto
{
    public PostSubscriptionDto(string customer, string billingType, double value, DateTime dueDate)
    {
        Customer = customer;
        BillingType = billingType;
        DueDate = dueDate;
        Value = value;
        Cycle = "MONTHLY";
    }
    [JsonProperty("customer")]
    public string Customer { get; set; }
    [JsonProperty("billingType")]
    public string BillingType { get; set; }
    [JsonProperty("value")]
    public double Value { get; set; }
    [JsonProperty("nextDueDate")] 
    public DateTime DueDate { get; set; }
    [JsonProperty("cycle")]
    public string Cycle { get; set; }
}