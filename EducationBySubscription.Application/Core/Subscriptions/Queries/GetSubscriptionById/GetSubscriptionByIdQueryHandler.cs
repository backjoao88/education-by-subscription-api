using EducationBySubscription.Application.Core.Subscriptions.Views;
using EducationSubscription.Core.Domain.Subscriptions.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Queries.GetSubscriptionById;

public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, Result<SubscriptionViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSubscriptionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SubscriptionViewModel>> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _unitOfWork.SubscriptionRepository.ReadById(request.Id);
        if (subscription is null) return Result.Fail<SubscriptionViewModel>(SubscriptionsErrors.SubscriptionNotFound);
        var subscriptionViewModel = new SubscriptionViewModel(subscription.Id, subscription.IdPlan, subscription.IdMember, subscription.Status);
        return Result.Ok(subscriptionViewModel);
    }
}