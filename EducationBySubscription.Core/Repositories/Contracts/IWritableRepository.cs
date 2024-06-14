using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Repositories.Contracts;

public interface IWritableRepository<in TEntity> where TEntity : Entity
{
    /// <summary>
    /// Adds an entity to a data repository.
    /// </summary>
    /// <param name="member"></param>
    public Task Add(TEntity member);
}