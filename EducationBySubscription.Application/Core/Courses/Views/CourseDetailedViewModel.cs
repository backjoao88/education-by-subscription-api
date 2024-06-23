namespace EducationBySubscription.Application.Core.Courses.Views;

public class CourseDetailedViewModel
{
    public CourseDetailedViewModel(Guid id, string name, string description, List<LessonViewModel> lessons, string cover)
    {
        Id = id;
        Name = name;
        Description = description;
        Lessons = lessons;
        Cover = cover;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Cover { get; set; }
    public List<LessonViewModel> Lessons { get; set; }
}