using EducationSubscription.Core.Domain.Courses;
using EducationSubscription.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private AppDbContext _appDbContext;

    public CourseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(Course member)
    {
        await _appDbContext.Courses.AddAsync(member);
    }

    public async Task<Course?> ReadById(Guid id)
    {
        return await _appDbContext.Courses.Include(o => o.Lessons).SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Course>> ReadAll()
    {
        return await _appDbContext.Courses.Include(o => o.Lessons).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        var course = await ReadById(id);
        if (course is null) return false;
        _appDbContext.Courses.Remove(course);
        return true;
    }
}