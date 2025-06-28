namespace Project1.Domain.Common;

public interface IAggregateRoot
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
