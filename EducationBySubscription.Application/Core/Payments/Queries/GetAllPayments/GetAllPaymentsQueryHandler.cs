using EducationBySubscription.Application.Core.Payments.Views;
using EducationBySubscription.Application.Core.Plans.Queries.GetPlanById;
using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Domain.Plans.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Payments.Queries.GetAllPayments;

public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, List<PaymentViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PaymentViewModel>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.PaymentRepository.ReadAll();
        var paymentViewModel = payment
            .Select(o => new PaymentViewModel(o.ExternalIdSubscription, o.Value))
            .ToList();
        return paymentViewModel;
    }
}