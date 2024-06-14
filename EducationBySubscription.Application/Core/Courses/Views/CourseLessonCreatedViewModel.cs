namespace EducationBySubscription.Application.Core.Courses.Views;

public class CourseLessonCreatedViewModel
{
    public CourseLessonCreatedViewModel(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}