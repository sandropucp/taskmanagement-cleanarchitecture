using System.Text.Json.Serialization;

namespace TaskManagement.Contracts.Tasks;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed
}
