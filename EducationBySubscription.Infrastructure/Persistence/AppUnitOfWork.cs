using EducationSubscription.Core.Repositories;

namespace EducationBySubscription.Infrastructure.Persistence;

/// <inheritdoc/>>
public class AppUnitOfWork : IUnitOfWork
{
    public AppUnitOfWork(AppDbContext appDbContext, ICourseRepository courseRepository, IMemberRepository memberRepository, IPlanRepository planRepository, ISubscriptionRepository subscriptionRepository, IPaymentRepository paymentRepository, IUserRepository userRepository)
    {
        _appDbContext = appDbContext;
        CourseRepository = courseRepository;
        MemberRepository = memberRepository;
        PlanRepository = planRepository;
        SubscriptionRepository = subscriptionRepository;
        PaymentRepository = paymentRepository;
        UserRepository = userRepository;
    }

    public ICourseRepository CourseRepository { get; set; }
    public IMemberRepository MemberRepository { get; set; }
    public IPlanRepository PlanRepository { get; set; }
    public ISubscriptionRepository SubscriptionRepository { get; set; }
    public IPaymentRepository PaymentRepository { get; set; } 
    public IUserRepository UserRepository { get; set; }
    
    private readonly AppDbContext _appDbContext;
    
    /// <inheritdoc/>>
    public async Task<int> Complete()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}