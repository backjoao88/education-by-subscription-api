using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Commands.CreatePlan;

public class CreatePlanCommand : IRequest<Result<PlanCreatedViewModel>>
{
    public CreatePlanCommand(string title, string description, decimal basePrice)
    {
        Title = title;
        Description = description;
        BasePrice = basePrice;
    }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal BasePrice { get; private set; }
}