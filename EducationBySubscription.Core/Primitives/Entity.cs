using EducationSubscription.Core.Primitives.Contracts;

namespace EducationSubscription.Core.Primitives;

/// <summary>
/// Represents an entity.
/// </summary>
public abstract class Entity : IEquatable<Guid>
{
    
    public Guid Id { get; protected set; } = Guid.NewGuid();
    private List<IDomainEvent> _events = new();
    public List<IDomainEvent> Events
    {
        get => _events;
        private set => _events = value;
    }

    /// <summary>
    /// Raises a new domain event into the entity.
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void Raise(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }
    
    public bool Equals(Guid other)
    {
        return Id == other;
    }
}