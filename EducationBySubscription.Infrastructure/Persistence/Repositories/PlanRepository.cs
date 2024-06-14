using EducationSubscription.Core.Domain.Plans;
using EducationSubscription.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly AppDbContext _appDbContext;

    public PlanRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(Plan plan)
    {
        await _appDbContext.Plans.AddAsync(plan);
    }

    public async Task<List<Plan>> ReadAll()
    {
        return await _appDbContext.Plans.ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        var plan = await ReadById(id);
        if (plan is null) return false;
        _appDbContext.Plans.Remove(plan);
        return true;
    }

    public async Task<Plan?> ReadById(Guid id)
    {
        return await _appDbContext.Plans.SingleOrDefaultAsync(o => o.Id == id);
    }
}