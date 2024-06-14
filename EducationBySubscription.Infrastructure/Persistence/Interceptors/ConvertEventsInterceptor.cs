using EducationBySubscription.Infrastructure.Persistence.Common;
using EducationSubscription.Core.Primitives;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace EducationBySubscription.Infrastructure.Persistence.Interceptors;

public class ConvertEventsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var outboxMessages = dbContext
            .ChangeTracker
            .Entries<Entity>()
            .Select(o => o.Entity)
            .SelectMany(o => o.Events)
            .Select(evt =>
            {
                return new OutboxMessage(
                    evt.GetType().Name,
                    JsonConvert.SerializeObject(evt, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
                );
            })
            .ToList();

        dbContext.AddRangeAsync(outboxMessages);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}