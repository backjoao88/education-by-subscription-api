using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Commands.DeletePlan;

public class DeletePlanCommand : IRequest<Result>
{
    public DeletePlanCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}