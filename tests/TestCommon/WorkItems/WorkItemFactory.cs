using TaskManagement.Domain.WorkItems;
using TestCommon.TestConstants;

namespace TestCommon.WorkItems;

public static class WorkItemFactory
{
    public static WorkItem CreateTask(
        Guid? id = null,
        string? name = null,
        string? description = null,
        DateTime? dueDate = null,
        WorkItemStatus? status = null,
        Guid? categoryId = null,
        string? categoryName = null,
        Guid? assignedToId = null,
        string? assignedToName = null) => new(
            name ?? "Test Task",
            description ?? "Test Description",
            dueDate ?? DateTime.Now,
            status ?? Constants.WorkItems.DefaultTaskStatus,
            categoryId ?? Guid.NewGuid(),
            categoryName ?? "Test Category",
            assignedToId ?? Guid.NewGuid(),
            assignedToName ?? "Test User",
            id ?? Constants.WorkItems.Id);
}