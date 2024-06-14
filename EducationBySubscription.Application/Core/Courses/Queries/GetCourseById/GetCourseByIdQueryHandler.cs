using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Primitives.Errors;
using EducationSubscription.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EducationBySubscription.Application.Core.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDetailedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCourseByIdQueryHandler> _logger;

    public GetCourseByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCourseByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<CourseDetailedViewModel>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.CourseRepository.ReadById(request.Id);
        if (course is null) return Result.Fail<CourseDetailedViewModel>(CourseErrors.Course.CourseNotFound);
        var courseViewModel = new CourseDetailedViewModel(course.Id, course.Name, course.Description, course.Lessons.Select(
            o =>
            {
                return new LessonViewModel(o.Name, o.Description);
            }).ToList());
        return Result.Ok(courseViewModel);
    }
}