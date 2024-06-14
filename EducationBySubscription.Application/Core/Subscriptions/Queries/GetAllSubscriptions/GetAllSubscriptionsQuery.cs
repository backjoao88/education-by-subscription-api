using EducationBySubscription.Application.Core.Subscriptions.Views;
using MediatR;

namespace EducationBySubscription.Application.Core.Subscriptions.Queries.GetAllSubscriptions;

public class GetAllSubscriptionsQuery : IRequest<List<SubscriptionViewModel>>;