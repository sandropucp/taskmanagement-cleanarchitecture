using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using TaskManagement.Application.Comments.Commands.CreateComment;
using TaskManagement.Application.Common.Behaviors;
using TaskManagement.Domain.Comments;
using TestCommon.Comments;

namespace TaskManagement.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateCommentCommand, ErrorOr<Comment>> _validationBehavior;
    private readonly IValidator<CreateCommentCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<Comment>> _mockNextBehavior;

    public ValidationBehaviorTests()
    {
        // Create a next behavior (mock)
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Comment>>>();

        // Create validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateCommentCommand>>();

        // Create validation behavior (SUT)
        _validationBehavior = new ValidationBehavior<CreateCommentCommand, ErrorOr<Comment>>(_mockValidator);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        // Arrange
        var createCommentRequest = CommentCommandFactory.CreateCreateCommentCommand();
        var comment = CommentFactory.CreateComment();

        _mockValidator
            .ValidateAsync(createCommentRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(comment);

        // Act
        var result = await _validationBehavior.Handle(createCommentRequest, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        var createTaskRequest = CommentCommandFactory.CreateCreateCommentCommand();
        List<ValidationFailure> validationFailures = [new(propertyName: "foo", errorMessage: "bad foo")];

        _mockValidator
            .ValidateAsync(createTaskRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _validationBehavior.Handle(createTaskRequest, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("foo");
        result.FirstError.Description.Should().Be("bad foo");
    }
}
