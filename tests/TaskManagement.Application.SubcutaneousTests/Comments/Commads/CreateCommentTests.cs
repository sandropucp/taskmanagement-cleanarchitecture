using ErrorOr;
using FluentAssertions;
using TaskManagement.Application.SubcutaneousTests.Common;
using TaskManagement.Domain.WorkItems;
using MediatR;
using TestCommon.Comments;
using TestCommon.WorkItems;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Categories;
using TestCommon.Categories;

namespace TaskManagement.Application.SubcutaneousTests.Comments.Commands;

[Collection(MediatorFactoryCollection.CollectionName)]
public class CreateCommentTests(MediatorFactory mediatorFactory)
{
    private readonly IMediator _mediator = mediatorFactory.CreateMediator();

    [Fact]
    public async Task CreateComment_WhenValidCommand_ShouldCreateComment()
    {
        // Arrange
        var user = await CreateUser();
        var category = await CreateCategory();
        var workItem = await CreateWorkItem(category.Id, user.Id);

        // Create a valid CreateGymCommand
        var createCommentCommand = CommentCommandFactory.CreateCreateCommentCommand(workItemId: workItem.Id, userId: user.Id);  

        // Act
        var createCommentResult = await _mediator.Send(createCommentCommand);

        // Assert
        createCommentResult.IsError.Should().BeFalse();
        createCommentResult.Value.WorkItemId.Should().Be(workItem.Id);
    }

    // [Theory]
    // [InlineData(0)]
    // [InlineData(1)]
    // [InlineData(200)]
    // public async Task CreateGym_WhenCommandContainsInvalidData_ShouldReturnValidationError(int gymNameLength)
    // {
    //     // Arrange
    //     string gymName = new('a', gymNameLength);
    //     var createGymCommand = GymCommandFactory.CreateCreateGymCommand(name: gymName);

    //     // Act
    //     var result = await _mediator.Send(createGymCommand);

    //     // Assert
    //     result.IsError.Should().BeTrue();
    //     result.FirstError.Code.Should().Be("Name");
    // }

    private async Task<WorkItem> CreateWorkItem(Guid categoryId, Guid userId)
    {
        //  1. Create a CreateSubscriptionCommand
        var createWorkItemCommand = WorkItemCommandFactory.CreateCreateWorkItemCommand(categoryId: categoryId, userAssignedToId: userId);

        //  2. Sending it to MediatR
        var result = await _mediator.Send(createWorkItemCommand);

        //  3. Making sure it was created successfully
        //result.IsError.Should().BeFalse();
        return result.Value;
    }

    private async Task<User> CreateUser()
    {
        //  1. Create a CreateSubscriptionCommand
        var createUserCommand = UserCommandFactory.CreateCreateUserCommand();

        //  2. Sending it to MediatR
        var result = await _mediator.Send(createUserCommand);

        //  3. Making sure it was created successfully
        //result.IsError.Should().BeFalse();
        return result.Value;
    }

    private async Task<Category> CreateCategory()
    {
        //  1. Create a CreateSubscriptionCommand
        var createCategoryCommand = CategoryCommandFactory.CreateCreateCategoryCommand();

        //  2. Sending it to MediatR
        var result = await _mediator.Send(createCategoryCommand);

        //  3. Making sure it was created successfully
        //result.IsError.Should().BeFalse();
        return result.Value;
    }
}