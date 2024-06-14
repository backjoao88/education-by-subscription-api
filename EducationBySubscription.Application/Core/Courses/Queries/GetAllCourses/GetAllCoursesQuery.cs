using EducationBySubscription.Application.Core.Courses.Views;
using MediatR;

namespace EducationBySubscription.Application.Core.Courses.Queries.GetAllCourses;

public class GetAllCoursesQuery : IRequest<List<CourseDetailedViewModel>>;