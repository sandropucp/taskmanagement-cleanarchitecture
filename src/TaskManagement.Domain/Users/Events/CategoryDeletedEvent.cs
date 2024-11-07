using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Users.Events;

public record CategoryDeletedEvent(Guid CategoryId) : IDomainEvent;