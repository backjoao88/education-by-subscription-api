using EducationBySubscription.Application.Core.Subscriptions.Views;
using EducationSubscription.Core.Domain.Subscriptions.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.SubscriptionRepository.Delete(request.Id);
        await _unitOfWork.Complete();
        if (!result) return Result.Fail<SubscriptionDeletedViewModel>(SubscriptionsErrors.SubscriptionNotFound);
        return Result.Ok(result);
    }
}