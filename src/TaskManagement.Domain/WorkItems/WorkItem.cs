using ErrorOr;
using TaskManagement.Domain.Attachments;
using TaskManagement.Domain.Categories;
using TaskManagement.Domain.Comments;
using TaskManagement.Domain.Common;
using Throw;

namespace TaskManagement.Domain.WorkItems;

public class WorkItem : Entity
{
    private readonly int maxAttachments = 10;
    private readonly int maxComments = 10;
    private readonly List<Guid> commentIds = [];
    private readonly List<Guid> attachmentIds = [];

    public string Name { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public Guid? CategoryId { get; private set; }
    public string? CategoryName { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public string? AssignedToName { get; private set; }
    public DateTime DueDate { get; private set; }
    public WorkItemStatus Status { get; private set; } = null!;
    private WorkItem() { }
    public WorkItem(
        string name,
        string? description,
        DateTime dueDate,
        WorkItemStatus workItemStatus,
        Guid categoryId,
        string categoryName,
        Guid assignedToId,
        string assignedToName,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        //Invarient: DueDate >= currentDate at the time of settin
        if (dueDate < DateTime.UtcNow)
        {
            //return WorkItemErrors.CannotHaveMoreCommentsThanAllows;
            throw new ArgumentException("Due date cannot be in the past");
        }

        Name = name;
        Description = description;
        DueDate = dueDate;
        Status = workItemStatus;
        CategoryId = categoryId;
        CategoryName = categoryName;
        AssignedToId = assignedToId;
        AssignedToName = assignedToName;
        Id = id ?? Guid.NewGuid();
    }
    public ErrorOr<Success> UpdateTask(WorkItem workItem)
    {
        DueDate = workItem.DueDate;
        Status = workItem.Status;
        CategoryId = workItem.CategoryId;
        CategoryName = workItem.CategoryName;
        AssignedToId = workItem.AssignedToId;
        AssignedToName = workItem.AssignedToName;

        return Result.Success;
    }
    public ErrorOr<Success> UpdateCategory(Category category)
    {
        CategoryId = category.Id;
        CategoryName = category.Name;
        return Result.Success;
    }
    public ErrorOr<Success> UpdateStatus(WorkItemStatus status)
    {
        if (Status == WorkItemStatus.Completed)
        {
            return WorkItemErrors.CannotUpdateStatusOfCompletedTask;
        }
        Status = status;
        return Result.Success;
    }
    public ErrorOr<Success> AddAttachment(Attachment attachment)
    {
        attachmentIds.Throw().IfContains(attachment.Id);
        if (attachmentIds.Count >= maxAttachments)
        {
            return WorkItemErrors.CannotHaveMoreAttachmentsThanAllows;
        }

        attachmentIds.Add(attachment.Id);
        return Result.Success;
    }
    public ErrorOr<Success> AddComment(Comment comment)
    {
        commentIds.Throw().IfContains(comment.Id);
        if (commentIds.Count >= maxComments)
        {
            return WorkItemErrors.CannotHaveMoreCommentsThanAllows;
        }
        commentIds.Add(comment.Id);
        return Result.Success;
    }
    public ErrorOr<Success> RemoveAttachment(Attachment attachment)
    {
        attachmentIds.Throw().IfNotContains(attachment.Id);

        attachmentIds.Remove(attachment.Id);
        return Result.Success;
    }
    public ErrorOr<Success> RemoveComment(Comment comment)
    {
        commentIds.Throw().IfNotContains(comment.Id);

        commentIds.Remove(comment.Id);
        return Result.Success;
    }
    public int GetMaxAttachments() => maxAttachments;
    public int GetMaxComments() => maxComments;

}
