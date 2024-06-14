using EducationBySubscription.Application.Core.Subscriptions.Commands.CreateSubscription;
using FluentValidation;

namespace EducationBySubscription.Application.Core.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public DeleteSubscriptionCommandValidator()
    {
        RuleFor(o => o.IdPlan).NotEmpty();
    }
}