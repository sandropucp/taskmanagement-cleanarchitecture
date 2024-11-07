using TaskManagement.Domain.Comments;
using TestCommon.TestConstants;

namespace TestCommon.Comments;

public static class CommentFactory
{
    public static Comment CreateComment(
        Guid? id = null,
        string? content = null,
        Guid? taskId = null,
        Guid? userId = null) => new(
            content ?? "Test Comment",
            taskId ?? Guid.NewGuid(),
            userId ?? Guid.NewGuid(),
            id ?? Constants.Comment.Id);
}