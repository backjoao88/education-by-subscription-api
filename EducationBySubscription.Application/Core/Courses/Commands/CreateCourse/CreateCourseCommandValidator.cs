using FluentValidation;

namespace EducationBySubscription.Application.Core.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(o => o.Name).NotEmpty().MaximumLength(30);
        RuleFor(o => o.Description).MaximumLength(200);
    }
}