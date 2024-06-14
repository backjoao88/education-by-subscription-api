using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Queries.GetAllCourses;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<CourseDetailedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCoursesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CourseDetailedViewModel>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _unitOfWork.CourseRepository.ReadAll();
        return courses
            .Select(o => new CourseDetailedViewModel(o.Id, o.Name, o.Description, o.Lessons.Select(l => new LessonViewModel(l.Name, l.Description)).ToList()))
            .ToList();
    }
}