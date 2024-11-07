namespace TaskManagement.Contracts.WorkItems;

public record UpdateWorkItemCategoryRequest(Guid WorkItemId, Guid CategoryId);
