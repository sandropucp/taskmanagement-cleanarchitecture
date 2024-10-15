namespace TaskManagement.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; init; }

    protected readonly List<IDomainEvent> domainEvents = [];
    protected Entity(Guid id) => Id = id;

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = domainEvents.ToList();

        domainEvents.Clear();

        return copy;
    }

    protected Entity()
    {
    }
}
