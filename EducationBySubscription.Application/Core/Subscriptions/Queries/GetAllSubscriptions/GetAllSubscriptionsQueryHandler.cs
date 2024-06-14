using EducationBySubscription.Application.Core.Subscriptions.Views;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Queries.GetAllSubscriptions;

public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, List<SubscriptionViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSubscriptionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<SubscriptionViewModel>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await _unitOfWork.SubscriptionRepository
            .ReadAll();
        var view = subscriptions
            .Select(o => new SubscriptionViewModel(o.Id, o.IdPlan, o.IdMember, o.Status))
            .ToList();
        return view;
    }
}