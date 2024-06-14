using MediatR;

namespace EducationBySubscription.Application.Core.Payments.Commands.ConfirmAsaasPayment;

public class ConfirmAsaasPaymentCommand : IRequest
{
    public ConfirmAsaasPaymentCommand(string externalSubscriptionId, decimal value)
    {
        ExternalSubscriptionId = externalSubscriptionId;
        Value = value;
    }
    public string ExternalSubscriptionId { get; set; }
    public decimal Value { get; set; }
}