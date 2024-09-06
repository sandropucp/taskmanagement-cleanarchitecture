using ErrorOr;

namespace TaskManagement.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error CannotNotHaveName = Error.Validation(
        code: "Task.CannotNotHaveName",
        description: "A task cannot not have name");

    public static readonly Error CannotHaveMoreAttachmentsThanAllows = Error.Validation(
        code: "Task.CannotHaveMoreAttachmentsThanAllows",
        description: "A task cannot have more attachments than the allows");

    public static readonly Error CannotUpdateStatusOfCompletedTask = Error.Validation(
        code: "Task.CannotUpdateStatusOfCompletedTask",
        description: "A task cannot update status of completed task");
}
