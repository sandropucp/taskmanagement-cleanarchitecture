using Ardalis.SmartEnum;

namespace TaskManagement.Domain.Tasks;

public class TaskStatus : SmartEnum<TaskStatus>
{
    public static readonly TaskStatus NotStarted = new(nameof(NotStarted), 0);
    public static readonly TaskStatus InProgress = new(nameof(InProgress), 1);
    public static readonly TaskStatus Completed = new(nameof(Completed), 2);

    public TaskStatus(string name, int value) : base(name, value)
    {
    }
}