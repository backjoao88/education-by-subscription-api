using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.CreateCourse;

public class CreateCourseCommand : IRequest<Result<CourseCreatedViewModel>>
{
    public CreateCourseCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; set; }
    public string Description { get; set; }
}