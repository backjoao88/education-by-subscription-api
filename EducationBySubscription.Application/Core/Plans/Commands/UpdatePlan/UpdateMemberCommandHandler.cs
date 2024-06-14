using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Domain.Plans.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Commands.UpdatePlan;

public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, Result<PlanUpdatedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PlanUpdatedViewModel>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _unitOfWork.PlanRepository.ReadById(request.Id);
        if (plan is null) return Result.Fail<PlanUpdatedViewModel>(PlanErrors.PlanNotFound);
        plan.Update(request.Title, request.Description, request.BasePrice);
        await _unitOfWork.Complete();
        var planUpdatedViewModel = new PlanUpdatedViewModel(plan.Id);
        return Result.Ok(planUpdatedViewModel);
    }
}