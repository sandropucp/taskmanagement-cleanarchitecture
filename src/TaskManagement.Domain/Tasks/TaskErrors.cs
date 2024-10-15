using ErrorOr;

namespace TaskManagement.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error CannotNotHaveName = Error.Validation(
        code: "Task.CannotNotHaveName",
        description: "A task cannot not have name");

    public static readonly Error CannotNotHaveStatus = Error.Validation(
        code: "Task.CannotNotHaveStatus",
        description: "A task cannot not have status");

    public static readonly Error CannotNotHaveCategory = Error.Validation(
        code: "Task.CannotNotHaveCategory",
        description: "A task cannot not have category");

    public static readonly Error CannotNotHaveAssignedUser = Error.Validation(
        code: "Task.CannotNotHaveAssignedUser",
        description: "A task cannot not have assigned user");

    public static readonly Error CannotHaveMoreAttachmentsThanAllows = Error.Validation(
        code: "Task.CannotHaveMoreAttachmentsThanAllows",
        description: "A task cannot have more attachments than the allows");

    public static readonly Error CannotUpdateStatusOfCompletedTask = Error.Validation(
        code: "Task.CannotUpdateStatusOfCompletedTask",
        description: "A task cannot update status of completed task");

    public static readonly Error TaskNotFound = Error.Validation(
        code: "Task.TaskNotFound",
        description: "Task not found");

    public static readonly Error CategoryNotFound = Error.Validation(
        code: "Task.CategoryNotFound",
        description: "Category not found");

    public static readonly Error AssignedUserNotFound = Error.Validation(
        code: "Task.AssignedUserNotFound",
        description: "Assigned user not found");

}
