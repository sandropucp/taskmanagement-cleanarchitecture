namespace TaskManagement.Contracts.Comments;

public record CommentResponse(Guid Id, Guid TaskId, Guid UserId, string CommentText);
