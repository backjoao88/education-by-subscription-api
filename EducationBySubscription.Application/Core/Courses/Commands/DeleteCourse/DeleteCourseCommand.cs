using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.DeleteCourse;

public class DeleteCourseCommand : IRequest<Result>
{
    public DeleteCourseCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}