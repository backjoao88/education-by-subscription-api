using EducationSubscription.Core.Domain.Payments;
using EducationSubscription.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _appDbContext;

    public PaymentRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(Payment member)
    {
        await _appDbContext.Payments.AddAsync(member);
    }

    public async Task<List<Payment>> ReadAll()
    {
        return await _appDbContext.Payments.ToListAsync();
    }

    public async Task<Payment?> ReadById(Guid id)
    {
        return await _appDbContext.Payments.Where(o => o.Id == id).FirstOrDefaultAsync();
    }
}