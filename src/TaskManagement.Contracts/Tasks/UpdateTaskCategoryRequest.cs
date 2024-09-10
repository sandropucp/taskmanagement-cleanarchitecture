namespace TaskManagement.Contracts.Tasks;

public record UpdateTaskCategoryRequest(Guid TaskId, Guid CategoryId);
