namespace Project1.Domain.Common;

public class AggregateRoot<T> : Entity<T>, IAggregateRoot
{
    private readonly List<BaseEvent> _domainEvents = new();
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(BaseEvent @event) => _domainEvents.Add(@event);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
