namespace EducationSubscription.Core.Repositories.Contracts;

public interface IDeletableRepository
{
    /// <summary>
    /// Deletes and entity from a repository.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> Delete(Guid id);
}