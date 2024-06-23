using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Domain.Members.Errors;
using EducationSubscription.Core.Domain.Users;
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
        var user = await _unitOfWork.UserRepository.ReadById(request.IdUser);
        if (user is null) return Result.Fail<CourseDetailedViewModel>(UserErrors.UserNotFound);
        
        if (user.UserRole == EUserRole.Student)
        {
            var member = await _unitOfWork.MemberRepository.ReadByEmail(user.Email);
            if (member is null) return Result.Fail<CourseDetailedViewModel>(MemberErrors.MemberNotFound); 
            var allowedCourses = await _unitOfWork.SubscriptionRepository.ReadCoursesByActiveMember(member.Id);
            if (!allowedCourses.Contains(request.Id)) return Result.Fail<CourseDetailedViewModel>(MemberErrors.MemberNotGranted); 
        }
        
        var course = await _unitOfWork.CourseRepository.ReadById(request.Id);
        if (course is null) return Result.Fail<CourseDetailedViewModel>(CourseErrors.Course.CourseNotFound);
        var courseViewModel = new CourseDetailedViewModel(course.Id, course.Name, course.Description, course.Lessons.Select(
            o =>
            {
                return new LessonViewModel(o.Name, o.Description);
            }).ToList(), 
            course.Cover
        );
        return Result.Ok(courseViewModel);
    }
}