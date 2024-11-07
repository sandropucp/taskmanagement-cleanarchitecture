using Ardalis.SmartEnum;

namespace TaskManagement.Domain.WorkItems;

public class WorkItemStatus(string name, int value) : SmartEnum<WorkItemStatus>(name, value)
{
    public static readonly WorkItemStatus NotStarted = new(nameof(NotStarted), 0);
    public static readonly WorkItemStatus InProgress = new(nameof(InProgress), 1);
    public static readonly WorkItemStatus Completed = new(nameof(Completed), 2);
}
