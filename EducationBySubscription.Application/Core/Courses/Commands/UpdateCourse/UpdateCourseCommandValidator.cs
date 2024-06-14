using FluentValidation;

namespace EducationBySubscription.Application.Core.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(o => o.Name).NotEmpty().MaximumLength(30);
        RuleFor(o => o.Description).MaximumLength(200);
    }
}