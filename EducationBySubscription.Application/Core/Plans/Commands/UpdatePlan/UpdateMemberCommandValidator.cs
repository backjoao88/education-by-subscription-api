using EducationBySubscription.Application.Core.Members.Commands.UpdateMember;
using FluentValidation;

namespace EducationBySubscription.Application.Core.Plans.Commands.UpdatePlan;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(o => o.FirstName).NotEmpty();
        RuleFor(o => o.LastName).NotEmpty();
        RuleFor(o => o.DocumentNumber).NotEmpty();
    }
}