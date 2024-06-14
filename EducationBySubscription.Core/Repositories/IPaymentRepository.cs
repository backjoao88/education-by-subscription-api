using EducationSubscription.Core.Domain.Payments;
using EducationSubscription.Core.Repositories.Contracts;

namespace EducationSubscription.Core.Repositories;

public interface IPaymentRepository : IWritableRepository<Payment>, IReadableAllRepository<Payment>,
    IReadableRepository<Payment>;