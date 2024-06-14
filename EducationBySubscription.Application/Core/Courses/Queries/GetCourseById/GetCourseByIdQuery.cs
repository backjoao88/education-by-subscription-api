using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Queries.GetCourseById;

public class GetCourseByIdQuery : IRequest<Result<CourseDetailedViewModel>>
{
    public GetCourseByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }   
}