using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Users.Commands.CreateUser;
using TaskManagement.Application.Users.Commands.DeleteUser;
using TaskManagement.Application.Users.Queries.GetUser;
using TaskManagement.Application.Users.Queries.ListUsers;
using TaskManagement.Contracts.Users;

namespace TaskManagement.Api.Controllers;

[Route("users")]
public class UsersController(ISender mediator) : ApiController
{
    private readonly ISender mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var command = new CreateUserCommand(request.Name, request.Email, request.Role);

        var createUserResult = await mediator.Send(command);
        return createUserResult.MatchFirst(
            user => Ok(new UserResponse(user.Id, user.Name)),
            Problem);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var query = new GetUserQuery(userId);

        var getUserResult = await mediator.Send(query);

        return getUserResult.Match(
            user => Ok(new UserResponse(
                user.Id,
                user.Name)),
            Problem);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var command = new DeleteUserCommand(userId);

        var deleteUserResult = await mediator.Send(command);

        return deleteUserResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {
        var command = new ListUsersQuery();

        var listUsersResult = await mediator.Send(command);

        return listUsersResult.Match(
            users => Ok(users.ConvertAll(user => new UserResponse(user.Id, user.Name))),
            Problem);
    }
}
