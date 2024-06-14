using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Domain.Plans.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Queries.GetPlanById;

public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, Result<PlanViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlanByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PlanViewModel>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var plan = await _unitOfWork.PlanRepository.ReadById(request.Id);
        if (plan is null) return Result.Fail<PlanViewModel>(PlanErrors.PlanNotFound);
        var planViewModel = new PlanViewModel(plan.Id, plan.Title, plan.Description, plan.BasePrice);
        return Result.Ok(planViewModel);
    }
}