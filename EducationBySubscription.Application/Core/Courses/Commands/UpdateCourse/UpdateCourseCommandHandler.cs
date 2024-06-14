using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<CourseUpdatedViewModel>>
{    
    private IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CourseUpdatedViewModel>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.CourseRepository.ReadById(request.Id);
        if (course is null) return Result.Fail<CourseUpdatedViewModel>(CourseErrors.Course.CourseNotFound);
        course.Update(request.Name, request.Description);
        await _unitOfWork.Complete();
        var courseUpdatedViewModel = new CourseUpdatedViewModel(course.Id);
        return Result.Ok(courseUpdatedViewModel);
    }
}