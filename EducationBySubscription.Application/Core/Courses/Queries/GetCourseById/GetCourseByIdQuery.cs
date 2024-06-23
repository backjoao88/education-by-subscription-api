using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Primitives;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Queries.GetCourseById;

public class GetCourseByIdQuery : IRequest<Result<CourseDetailedViewModel>>
{
    public GetCourseByIdQuery(Guid id, Guid idUser)
    {
        Id = id;
        IdUser = idUser;
    }
    public Guid Id { get; set; }   
    public Guid IdUser { get; set; }
}