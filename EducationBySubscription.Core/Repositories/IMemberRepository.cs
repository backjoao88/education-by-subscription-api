using EducationSubscription.Core.Domain.Members;
using EducationSubscription.Core.Repositories.Contracts;

namespace EducationSubscription.Core.Repositories;

public interface IMemberRepository : IWritableRepository<Member>, IReadableAllRepository<Member>, IDeletableRepository,
    IReadableRepository<Member>
{
    public Task<Member?> ReadByEmail(string email);
}
