namespace TaskManagement.Contracts.WorkItems;

public record WorkItemResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime DueDate,
    string Status,
    string? CategoryName);
