using ErrorOr;
using TaskManagement.Domain.Attachments;
using TaskManagement.Domain.Categories;
using TaskManagement.Domain.Comments;
using Throw;

namespace TaskManagement.Domain.Tasks;

public class Task
{
    private readonly int maxAttachments = 10;
    private readonly List<Guid> commentIds = [];
    private readonly List<Guid> attachmentIds = [];
    public Guid Id { get; private set; }
    public string? Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Guid? CategoryId { get; private set; }
    public string? CategoryName { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public string? AssignedToName { get; private set; }
    public DateTime DueDate { get; private set; }
    public TaskStatus Status { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string CreatedBy { get; private set; } = null!;
    public DateTime UpdatedAt { get; private set; }
    public string UpdatedBy { get; private set; } = null!;

    private Task() { }
    public Task(string name, string description, DateTime dueDate,
        TaskStatus taskStatus, Guid categoryId, string categoryName,
        Guid assignedToId, Guid? id = null)
    {
        Name = name;
        Description = description;
        DueDate = dueDate;
        Status = taskStatus;
        CategoryId = categoryId;
        CategoryName = categoryName;
        AssignedToId = assignedToId;
        Id = id ?? Guid.NewGuid();
    }
    public ErrorOr<Success> UpdateTask(Task task)
    {
        DueDate = task.DueDate;
        Status = task.Status;
        CategoryId = task.CategoryId;
        CategoryName = task.CategoryName;
        AssignedToId = task.AssignedToId;

        return Result.Success;
    }
    public ErrorOr<Success> UpdateCategory(Category category)
    {
        CategoryId = category.Id;
        CategoryName = category.Name;
        return Result.Success;
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
    public ErrorOr<Success> AddAttachment(Attachment attachment)
    {
        attachmentIds.Throw().IfContains(attachment.Id);

        if (attachmentIds.Count >= maxAttachments)
        {
            return TaskErrors.CannotHaveMoreAttachmentsThanAllows;
        }

        attachmentIds.Add(attachment.Id);

        return Result.Success;
    }
    public ErrorOr<Success> RemoveAttachment(Attachment attachment)
    {
        attachmentIds.Throw().IfNotContains(attachment.Id);

        attachmentIds.Remove(attachment.Id);

        return Result.Success;
    }
    public ErrorOr<Success> AddComment(Comment comment)
    {
        commentIds.Throw().IfContains(comment.Id);

        commentIds.Add(comment.Id);

        return Result.Success;
    }
    public ErrorOr<Success> RemoveComment(Comment comment)
    {
        commentIds.Throw().IfNotContains(comment.Id);

        commentIds.Remove(comment.Id);

        return Result.Success;
    }
    public ErrorOr<Success> UpdateComment(Comment comment)
    {
        commentIds.Throw().IfNotContains(comment.Id);

        return Result.Success;
    }
}
