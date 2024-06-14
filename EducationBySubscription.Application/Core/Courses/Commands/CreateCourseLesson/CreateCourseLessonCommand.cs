using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.CreateCourseLesson;

public class CreateCourseLessonCommand : IRequest<Result<CourseLessonCreatedViewModel>>
{
    public CreateCourseLessonCommand(Guid idCourse, string name, string description)
    {
        IdCourse = idCourse;
        Name = name;
        Description = description;
    }
    public Guid IdCourse { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}