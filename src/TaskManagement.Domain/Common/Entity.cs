namespace TaskManagement.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; init; }
    protected readonly List<IDomainEvent> domainEvents = [];
    protected Entity() { }
    protected Entity(Guid id) => Id = id;

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = domainEvents.ToList();

        domainEvents.Clear();

        return copy;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }
        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

}
