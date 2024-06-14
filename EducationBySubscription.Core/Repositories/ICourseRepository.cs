using EducationSubscription.Core.Domain.Courses;
using EducationSubscription.Core.Repositories.Contracts;

namespace EducationSubscription.Core.Repositories;

public interface ICourseRepository : IWritableRepository<Course>, IReadableRepository<Course>, IReadableAllRepository<Course>, IDeletableRepository;