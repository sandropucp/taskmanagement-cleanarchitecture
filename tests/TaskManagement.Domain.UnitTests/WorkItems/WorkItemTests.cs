using TestCommon.Comments;
using TaskManagement.Domain.WorkItems;
using TestCommon.WorkItems;
using FluentAssertions;
using ErrorOr;

namespace TaskManagement.Domain.UnitTests.Tasks;

public class WorkItemTests
{
    [Fact]
    public void AddAttachment_WhenMoreThanNumberAllow_ShouldFail()
    {
        // Arrange
        var task = WorkItemFactory.CreateTask();

        // Create the maximum number of comments + 1
        var comments = Enumerable.Range(0, task.GetMaxComments() + 1)
            .Select(_ => CommentFactory.CreateComment(id: Guid.NewGuid()))
            .ToList();

        // Act
        var addCommentResults = comments.ConvertAll(task.AddComment);

        // Assert
        var allButLastCommentResults = addCommentResults[..^1];
        addCommentResults.Should().AllSatisfy(addCommentResult => addCommentResult.Value.Should().Be(Result.Success));

        var lastAddCommentResult = addCommentResults.Last();
        lastAddCommentResult.IsError.Should().BeTrue();
        lastAddCommentResult.FirstError.Should().Be(WorkItemErrors.CannotHaveMoreCommentsThanAllows);
    }

    [Fact]
    public void CreateWorkItem_WhenDueDateIsInThePast_ShouldFail()
    {
        // Arrange
        var dueDate = DateTime.UtcNow.AddDays(-1);

        // Act
        Action act = () => WorkItemFactory.CreateTask(dueDate: dueDate);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage(WorkItemErrors.DueDateCannotBeInThePast.Description);
    }

}