using EducationSubscription.Core.Domain.Payments;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Payments.Commands.ConfirmAsaasPayment;

public class ConfirmAsaasPaymentCommandHandler : IRequestHandler<ConfirmAsaasPaymentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmAsaasPaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ConfirmAsaasPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = Payment.Create(request.ExternalSubscriptionId, request.Value);
        await _unitOfWork.PaymentRepository.Add(payment);
        await _unitOfWork.Complete();
    }
}