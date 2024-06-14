using EducationBySubscription.Application.Core.Courses.Commands.CreateCourse;
using FluentValidation;

namespace EducationBySubscription.Application.Core.Courses.Commands.CreateCourseLesson;

public class CreateCourseLessonCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseLessonCommandValidator()
    {
        RuleFor(o => o.Name).NotEmpty().MaximumLength(30);
        RuleFor(o => o.Description).MaximumLength(200);
    }
}