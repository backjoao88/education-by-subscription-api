using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand : IRequest<Result<CourseUpdatedViewModel>>
{
    public UpdateCourseCommand(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}