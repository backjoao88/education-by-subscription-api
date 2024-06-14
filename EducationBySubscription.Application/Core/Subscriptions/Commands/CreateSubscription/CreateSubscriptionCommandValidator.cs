using FluentValidation;

namespace EducationBySubscription.Application.Core.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(o => o.IdPlan).NotEmpty();
    }
}