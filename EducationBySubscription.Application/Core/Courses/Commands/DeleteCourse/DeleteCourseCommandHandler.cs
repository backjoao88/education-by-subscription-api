using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.CourseRepository.Delete(request.Id);
        await _unitOfWork.Complete();
        if (!result) return Result.Fail(CourseErrors.Course.CourseNotFound);
        return Result.Ok(result);
    }
}