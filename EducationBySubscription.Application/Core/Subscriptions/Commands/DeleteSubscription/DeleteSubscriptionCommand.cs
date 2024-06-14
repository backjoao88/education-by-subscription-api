using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommand : IRequest<Result>
{
    public DeleteSubscriptionCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}