using ErrorOr;
using TaskManagement.Domain.Attachments;
using TaskManagement.Domain.Comments;
using Throw;

namespace TaskManagement.Domain.Tasks;

public class Task
{
    private readonly List<Guid> _commentIds = [];
    private readonly List<Guid> _attachmentIds = [];
    private readonly int _maxAttachments = 10;
    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime DueDate { get; init; }
    public TaskStatus Status { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    private Task() { }
    public Task(string name, string description, DateTime dueDate,
        TaskStatus taskStatus, Guid categoryId, Guid assignedToId,
        Guid? id = null)
    {
        Name = name;
        Description = description;
        DueDate = dueDate;
        Status = taskStatus;
        CategoryId = categoryId;
        AssignedToId = assignedToId;
        Id = id ?? Guid.NewGuid();
    }
    public ErrorOr<Success> UpdateStatus(TaskStatus status)
    {
        if (Status == TaskStatus.Completed)
        {
            return TaskErrors.CannotUpdateStatusOfCompletedTask;
        }

        Status = status;

        return Result.Success;
    }
    public ErrorOr<Success> AddComment(Comment comment)
    {
        _commentIds.Throw().IfContains(comment.Id);
        _commentIds.Add(comment.Id);

        return Result.Success;
    }
    public ErrorOr<Success> AddAttachment(Attachment attachment)
    {
        _attachmentIds.Throw().IfContains(attachment.Id);

        if (_attachmentIds.Count >= _maxAttachments)
        {
            return TaskErrors.CannotHaveMoreAttachmentsThanAllows;
        }

        _attachmentIds.Add(attachment.Id);

        return Result.Success;
    }

}
