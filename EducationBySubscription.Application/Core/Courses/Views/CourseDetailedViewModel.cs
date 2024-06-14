namespace EducationBySubscription.Application.Core.Courses.Views;

public class CourseDetailedViewModel
{
    public CourseDetailedViewModel(Guid id, string name, string description, List<LessonViewModel> lessons)
    {
        Id = id;
        Name = name;
        Description = description;
        Lessons = lessons;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<LessonViewModel> Lessons { get; set; }
}