namespace EducationBySubscription.Infrastructure.Persistence.Common;

public class OutboxMessage
{
    public OutboxMessage(string type, string content)
    {
        Type = type;
        Content = content;
        Processed = false;
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
    public Guid Id { get; private set; }
    public string Type { get; private set; } 
    public string Content { get; private set; }
    public bool Processed { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void Process()
    {
        Processed = true;
    }
    
}