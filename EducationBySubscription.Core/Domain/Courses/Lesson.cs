using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Courses;

public class Lesson : Entity
{
    private Lesson(Guid idCourse, string name, string description, string link)
    {
        Name = name;
        Description = description;
        Link = link;
        IdCourse = idCourse;
    }
    
    public static Lesson Create(Guid idCourse, string name, string description)
    {
        var lesson = new Lesson(idCourse, name, description, "");
        return lesson;
    }
    
    public Guid IdCourse { get; private set; }
    public Course Course { get; private set; } = null!;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Link { get; private set; }
}