namespace TaskManagement.Domain.Attachments;
public class Attachment
{
    public Guid Id { get; private set; }
    public Guid WorkItemId { get; private set; }
    public string FileName { get; private set; } = null!;

    public Attachment() { }
    public Attachment(string fileName, Guid workItemId, Guid? id = null)
    {
        FileName = fileName;
        WorkItemId = workItemId;
        Id = id ?? Guid.NewGuid();
    }
}
