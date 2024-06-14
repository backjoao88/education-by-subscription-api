using EducationBySubscription.Application.Core.Subscriptions.Views;
using EducationSubscription.Core.Domain.Members.Errors;
using EducationSubscription.Core.Domain.Plans.Errors;
using EducationSubscription.Core.Domain.Subscriptions;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Result<SubscriptionCreatedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SubscriptionCreatedViewModel>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var plan = await _unitOfWork.PlanRepository.ReadById(request.IdPlan);
        if (plan is null)
        {
            return Result.Fail<SubscriptionCreatedViewModel>(PlanErrors.PlanNotFound);
        }
        var member = await _unitOfWork.MemberRepository.ReadById(request.IdMember);
        if (member is null)
        {
            return Result.Fail<SubscriptionCreatedViewModel>(MemberErrors.MemberNotFound);
        }
        var subscription = Subscription.Create(plan.Id, member.Id);
        await _unitOfWork.SubscriptionRepository.Add(subscription);
        await _unitOfWork.Complete();
        return Result.Ok(new SubscriptionCreatedViewModel(subscription.Id));
    }
}