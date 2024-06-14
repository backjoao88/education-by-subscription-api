using EducationBySubscription.Application.Core.Plans.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Plans.Commands.UpdatePlan;

public class UpdatePlanCommand : IRequest<Result<PlanUpdatedViewModel>>
{
    public UpdatePlanCommand(Guid id, string title, string description, decimal basePrice)
    {
        Id = id;
        Title = title;
        Description = description;
        BasePrice = basePrice;
    }
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal BasePrice { get; set; }

}