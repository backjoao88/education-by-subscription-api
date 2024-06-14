using EducationBySubscription.Application.Core.Members.Commands.DeleteMember;
using EducationSubscription.Core.Domain.Plans.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Commands.DeletePlan;

public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.PlanRepository.Delete(request.Id);
        await _unitOfWork.Complete();
        if (!result) return Result.Fail(PlanErrors.PlanNotFound);
        return Result.Ok(result);
    }
}