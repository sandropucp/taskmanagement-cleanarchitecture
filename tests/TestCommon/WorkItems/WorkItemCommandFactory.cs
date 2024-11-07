using TaskManagement.Application.WorkItems.Commands.CreateWorkItem;
using TaskManagement.Domain.WorkItems;
using TestCommon.TestConstants;

namespace TestCommon.WorkItems;

public static class WorkItemCommandFactory
{
    public static CreateWorkItemCommand CreateCreateWorkItemCommand(
        string name = null,
        string description = null,
        DateTime? dueDate = null,
        WorkItemStatus? taskStatus = null,
        Guid? categoryId = null,
        Guid? userAssignedToId = null) => new(
            Name: name ?? Constants.WorkItems.DefaultName,
            Description: description ?? Constants.WorkItems.DefaultDescription,
            DueDate: dueDate ?? Constants.WorkItems.DefaultDueDate,
            TaskStatus: taskStatus ?? Constants.WorkItems.DefaultTaskStatus,
            CategoryId: categoryId ?? Constants.WorkItems.DefaultCategoryId,
            UserAssignedToId: userAssignedToId ?? Constants.WorkItems.DefaultUserAssignedToId);
}