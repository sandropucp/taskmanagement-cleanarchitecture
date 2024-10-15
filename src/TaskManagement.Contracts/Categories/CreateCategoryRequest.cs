namespace TaskManagement.Contracts.Categories;

public record CreateCategoryRequest(string Name, Guid AdminId);
