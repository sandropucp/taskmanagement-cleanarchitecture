using ErrorOr;

namespace TaskManagement.Domain.WorkItems;

public static class WorkItemErrors
{
    public static readonly Error CannotNotHaveName = Error.Validation(
        code: "WorkItem.CannotNotHaveName",
        description: "A task cannot not have name");

    public static readonly Error CannotNotHaveStatus = Error.Validation(
        code: "WorkItem.CannotNotHaveStatus",
        description: "A task cannot not have status");

    public static readonly Error CannotNotHaveCategory = Error.Validation(
        code: "WorkItem.CannotNotHaveCategory",
        description: "A task cannot not have category");

    public static readonly Error CannotNotHaveAssignedUser = Error.Validation(
        code: "WorkItem.CannotNotHaveAssignedUser",
        description: "A task cannot not have assigned user");

    public static readonly Error CannotHaveMoreAttachmentsThanAllows = Error.Validation(
        code: "WorkItem.CannotHaveMoreAttachmentsThanAllows",
        description: "A task cannot have more attachments than the allows");

    public static readonly Error CannotHaveMoreCommentsThanAllows = Error.Validation(
        code: "WorkItem.CannotHaveMoreCommentsThanAllows",
        description: "A task cannot have more comments than the allows");

    public static readonly Error CannotUpdateStatusOfCompletedTask = Error.Validation(
        code: "WorkItem.CannotUpdateStatusOfCompletedTask",
        description: "A task cannot update status of completed task");

    public static readonly Error TaskNotFound = Error.Validation(
        code: "WorkItem.TaskNotFound",
        description: "Task not found");

    public static readonly Error CategoryNotFound = Error.Validation(
        code: "WorkItem.CategoryNotFound",
        description: "Category not found");

    public static readonly Error AssignedUserNotFound = Error.Validation(
        code: "WorkItem.AssignedUserNotFound",
        description: "Assigned user not found");

    public static readonly Error DueDateCannotBeInThePast = Error.Validation(
        code: "WorkItem.DueDateCannotBeInThePast",
        description: "Due date cannot be in the past");

}
