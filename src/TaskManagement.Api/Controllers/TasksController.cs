using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Contracts.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainTaskStatus = TaskManagement.Domain.Tasks.TaskStatus;

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
        if (!DomainTaskStatus.TryFromName(
            request.TaskStatus.ToString(),
            out var taskStatus))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid task status");
        }

        var command = new CreateTaskCommand(request.Name, request.Description,
            request.DueDate, taskStatus, request.CategoryId, request.UserId);

        var createTaskResult = await _mediator.Send(command);
        return createTaskResult.MatchFirst(
            task => Ok(new TaskResponse(task.Id, task.Name)),
            error => Problem(error));
    }
}