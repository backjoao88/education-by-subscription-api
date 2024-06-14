namespace EducationBySubscription.Application.Core.Courses.Views;

public class CourseUpdatedViewModel
{
    public CourseUpdatedViewModel(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}