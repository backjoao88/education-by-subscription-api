using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Domain.Courses;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<CourseCreatedViewModel>>
{
    private IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CourseCreatedViewModel>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.Create(request.Name, request.Description);
        await _unitOfWork.CourseRepository.Add(course);
        await _unitOfWork.Complete();
        var courseCreatedViewModel = new CourseCreatedViewModel(course.Id);
        return Result.Ok(courseCreatedViewModel);
    }
}