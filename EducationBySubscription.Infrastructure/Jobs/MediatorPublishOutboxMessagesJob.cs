using EducationBySubscription.Infrastructure.Persistence;
using EducationSubscription.Core.Primitives.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;

namespace EducationBySubscription.Infrastructure.Jobs;

public class MediatorPublishOutboxMessagesJob : IJob
{

    private readonly IServiceProvider _provider;

    public MediatorPublishOutboxMessagesJob(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _provider.CreateAsyncScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        
        var outboxMessages = appDbContext
            .OutboxMessages
            .Where(o => !o.Processed)
            .ToList();
        
        foreach (var outboxMessage in outboxMessages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            if (domainEvent is null) continue;
            await publisher.Publish(domainEvent);
            outboxMessage.Process();
        }
        await appDbContext.SaveChangesAsync();
    }
}