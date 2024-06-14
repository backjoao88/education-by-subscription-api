namespace EducationSubscription.Core.Repositories;

/// <summary>
/// Represents the unit of work pattern.
/// </summary>
public interface IUnitOfWork
{
    public ICourseRepository CourseRepository { get; set; }
    public IMemberRepository MemberRepository { get; set; }
    public IPlanRepository PlanRepository { get; set; }
    public ISubscriptionRepository SubscriptionRepository { get; set; }
    public IPaymentRepository PaymentRepository { get; set; } 
    public IUserRepository UserRepository { get; set; }
    
    /// <summary>
    /// Completes a transaction.
    /// </summary>
    /// <returns></returns>
    public Task<int> Complete();
}