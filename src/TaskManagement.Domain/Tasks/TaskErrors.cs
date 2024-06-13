using ErrorOr;

namespace TaskManagement.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error CannotNotHaveName = Error.Validation(
        code: "Task.CannotNotHaveName",
        description: "A task cannot not have name");
}