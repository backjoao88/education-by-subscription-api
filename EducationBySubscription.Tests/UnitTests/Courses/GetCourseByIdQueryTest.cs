using EducationBySubscription.Application.Core.Courses.Queries.GetCourseById;
using EducationSubscription.Core.Repositories;

namespace EducationBySubscription.Tests.UnitTests.Courses;

public class GetCourseByIdCommandTest
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCourseByIdCommandTest(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Fact]
    public void ShouldReturnErrorWhenCourseIsNotAllowed()
    {
        // Arrange
        var command = new GetCourseByIdQuery();
    }
}