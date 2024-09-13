namespace TaskManagement.Infrastructure.Common.Persistence;

public class AuditEntry
{
    public Guid Id { get; set; }
    public string Metadata { get; set; } = null!;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public bool Succeded { get; set; }
    public string ErrorMessage { get; set; } = null!;
}
