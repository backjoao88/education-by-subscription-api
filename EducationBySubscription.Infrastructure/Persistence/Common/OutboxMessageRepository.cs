using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence.Common;

public class OutboxMessageRepository
{
    private readonly AppDbContext _appDbContext;

    public OutboxMessageRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(OutboxMessage outboxMessage)
    {
        await _appDbContext.OutboxMessages.AddAsync(outboxMessage);
    }

    public async Task<OutboxMessage?> ReadById(Guid id)
    {
        return await _appDbContext.OutboxMessages.SingleOrDefaultAsync(o => o.Id == id);
    }
    
}