using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Domain.Courses;
using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Commands.CreateCourseLesson;

public class CreateCourseLessonCommandHandler : IRequestHandler<CreateCourseLessonCommand, Result<CourseLessonCreatedViewModel>>
{
    private IUnitOfWork _unitOfWork;

    public CreateCourseLessonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CourseLessonCreatedViewModel>> Handle(CreateCourseLessonCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.CourseRepository.ReadById(request.IdCourse);
        if (course is null)
        {
            return Result.Fail<CourseLessonCreatedViewModel>(CourseErrors.Course.CourseNotFound);
        }
        var lesson = Lesson.Create(course.Id, request.Name, request.Description);
        course.AddLesson(lesson);
        await _unitOfWork.Complete();
        var courseLessonCreatedViewModel = new CourseLessonCreatedViewModel(lesson.Id);
        return Result.Ok(courseLessonCreatedViewModel);
    }
}