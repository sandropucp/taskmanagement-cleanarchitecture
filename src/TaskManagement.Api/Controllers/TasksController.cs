using ErrorOr;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Contracts.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Api.Controllers;

[Route("tasks")]
public class TasksController : ApiController
{
    private readonly ISender _mediator;

    public TasksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(
        CreateTaskRequest request)
    {
        var command = new CreateTaskCommand(
            request.Name);

        var createTaskResult = await _mediator.Send(command);
        return createTaskResult.MatchFirst(
            task => Ok(new TaskResponse(task.Id, task.Name)),
            error => Problem(error));
    }
}