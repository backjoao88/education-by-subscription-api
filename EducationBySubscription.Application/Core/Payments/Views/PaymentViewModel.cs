namespace EducationBySubscription.Application.Core.Payments.Views;

public class PaymentViewModel
{
    public PaymentViewModel(string externalIdSubscription, decimal value)
    {
        ExternalIdSubscription = externalIdSubscription;
        Value = value;
    }
    public string ExternalIdSubscription { get; set; }
    public decimal Value { get; set; }
}