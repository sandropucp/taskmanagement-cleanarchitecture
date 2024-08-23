namespace TaskManagement.Domain.Attachments;
public class Attachment
{
    public Guid Id { get; private set; }
    public Guid TaskId { get; private set; }
    public string FileName { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    public Attachment() { }
    public Attachment(string fileName, Guid taskId, Guid? id = null)
    {
        FileName = fileName;
        TaskId = taskId;
        Id = id ?? Guid.NewGuid();
    }
}