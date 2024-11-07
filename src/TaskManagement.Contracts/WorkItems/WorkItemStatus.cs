using System.Text.Json.Serialization;

namespace TaskManagement.Contracts.WorkItems;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkItemStatus
{
    NotStarted,
    InProgress,
    Completed
}
