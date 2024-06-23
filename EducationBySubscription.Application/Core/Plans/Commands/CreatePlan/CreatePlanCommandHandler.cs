using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Domain.Plans;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Commands.CreatePlan;

public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Result<PlanCreatedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PlanCreatedViewModel>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = new Plan(request.Title, request.Description, request.BasePrice, request.AllowedCourses);
        await _unitOfWork.PlanRepository.Add(plan);
        await _unitOfWork.Complete();
        var planCreatedViewModel = new PlanCreatedViewModel(plan.Id);
        return Result.Ok(planCreatedViewModel);
    }
}