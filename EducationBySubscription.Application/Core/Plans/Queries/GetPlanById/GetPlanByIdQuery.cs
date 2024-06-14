using EducationBySubscription.Application.Core.Members.Views;
using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Queries.GetPlanById;

public class GetPlanByIdQuery : IRequest<Result<PlanViewModel>>
{
    public GetPlanByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}