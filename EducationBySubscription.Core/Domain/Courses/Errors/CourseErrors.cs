using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Courses.Errors;

public class CourseErrors
{
    public static class Course
    {
        public static Error CourseNotFound = new("Course.NotFound", "This course was not found.");
    }
    public static class Lesson
    {
        public static Error LessonNotFound = new("Course.LessonNotFound", "This lesson was not found.");
    }
    public static class Module
    {
        public static Error ModuleNotFound = new("Course.ModuleNotFound", "This module was not found.");
    }
}