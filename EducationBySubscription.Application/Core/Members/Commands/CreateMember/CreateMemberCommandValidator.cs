using FluentValidation;

namespace EducationBySubscription.Application.Core.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(o => o.FirstName).NotEmpty();
        RuleFor(o => o.LastName).NotEmpty();
        RuleFor(o => o.DocumentNumber).NotEmpty();
        RuleFor(o => o.Email).NotEmpty().EmailAddress();
    }
}