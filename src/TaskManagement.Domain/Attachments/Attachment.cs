using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Attachments;

public class Attachment : Entity
{
    public Guid WorkItemId { get; private set; }
    public string FileName { get; private set; } = null!;

    public Attachment() { }
    public Attachment(
        string fileName,
        Guid workItemId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        FileName = fileName;
        WorkItemId = workItemId;
        Id = id ?? Guid.NewGuid();
    }
}
