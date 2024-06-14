namespace EducationBySubscription.Application.Core.Courses.Views;

public class LessonViewModel
{
    public LessonViewModel(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; set; }
    public string Description { get; set; }
}