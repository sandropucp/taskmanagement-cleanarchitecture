using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.AuditEntries;

public class AuditEntry : Entity
{
    public string Metadata { get; set; } = null!;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public bool Succeded { get; set; }
    public string ErrorMessage { get; set; } = null!;
}
