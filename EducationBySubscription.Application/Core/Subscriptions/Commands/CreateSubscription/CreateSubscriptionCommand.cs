using EducationBySubscription.Application.Core.Subscriptions.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommand : IRequest<Result<SubscriptionCreatedViewModel>>
{
    public CreateSubscriptionCommand(Guid idPlan, Guid idMember)
    {
        IdPlan = idPlan;
        IdMember = idMember;
    }
    public Guid IdPlan { get; set; }
    public Guid IdMember { get; set; }
}