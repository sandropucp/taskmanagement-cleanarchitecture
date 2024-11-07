namespace TaskManagement.Contracts.WorkItems;

public record CreateWorkItemRequest(
    string Name,
    string Description,
    DateTime DueDate,
    WorkItemStatus WorkItemStatus,
    Guid CategoryId,
    Guid UserAssignedToId);
