using FluentValidation;

namespace EducationBySubscription.Application.Core.Plans.Commands.CreatePlan;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(o => o.Title).MaximumLength(100).NotEmpty();
        RuleFor(o => o.Description).MaximumLength(500).NotEmpty();
        RuleFor(o => o.BasePrice).NotEmpty();
    }
}