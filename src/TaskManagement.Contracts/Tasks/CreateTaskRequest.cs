namespace TaskManagement.Contracts.Tasks;

public record CreateTaskRequest(string Name, string Description, DateTime DueDate,
    TaskStatus TaskStatus, Guid CategoryId, Guid UserId);
