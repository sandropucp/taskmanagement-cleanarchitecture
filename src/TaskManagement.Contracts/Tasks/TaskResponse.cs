namespace TaskManagement.Contracts.Tasks;

public record TaskResponse(Guid Id, string Name, string Description, DateTime DueDate, string Status, string? CategoryName);
