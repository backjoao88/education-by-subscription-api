using EducationSubscription.Core.Domain.Plans;
using EducationSubscription.Core.Repositories.Contracts;

namespace EducationSubscription.Core.Repositories;

public interface IPlanRepository : IWritableRepository<Plan>, IReadableAllRepository<Plan>, IDeletableRepository,
    IReadableRepository<Plan>;