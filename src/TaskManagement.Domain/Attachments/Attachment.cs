namespace TaskManagement.Domain.Attachments;
public class Attachment
{
    public Guid Id { get; private set; }
    public Guid TaskId { get; private set; }
    public string FileName { get; private set; } = null!;

    public Attachment() { }
    public Attachment(string fileName, Guid taskId, Guid? id = null)
    {
        FileName = fileName;
        TaskId = taskId;
        Id = id ?? Guid.NewGuid();
    }
}
