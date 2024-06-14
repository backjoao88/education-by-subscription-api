using EducationBySubscription.Application.Core.Subscriptions.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Queries.GetSubscriptionById;

public class GetSubscriptionByIdQuery : IRequest<Result<SubscriptionViewModel>>
{
    public GetSubscriptionByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}