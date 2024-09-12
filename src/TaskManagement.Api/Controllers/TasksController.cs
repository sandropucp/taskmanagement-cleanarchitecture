using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Tasks.Commands.UpdateTaskCategory;
using TaskManagement.Application.Tasks.Queries.GetTask;
using TaskManagement.Application.Tasks.Queries.ListTasks;
using TaskManagement.Contracts.Tasks;
using DomainTaskStatus = TaskManagement.Domain.Tasks.TaskStatus;

namespace TaskManagement.Api.Controllers;

[Route("tasks")]
public class TasksController(ISender mediator) : ApiController
{
    private readonly ISender mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request)
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

        var createTaskResult = await mediator.Send(command);
        return createTaskResult.MatchFirst(
            task => Ok(new TaskResponse(task.Id, task.Name, task.Description,
                task.DueDate, task.Status.ToString(), task.CategoryName)),
            Problem);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var query = new GetTaskQuery(taskId);

        var getTaskResult = await mediator.Send(query);

        return getTaskResult.Match(
            task => Ok(new TaskResponse(
                task.Id,
                task.Name, task.Description, task.DueDate, task.Status.ToString(), task.CategoryName)),
            Problem);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        var command = new DeleteTaskCommand(taskId);

        var deleteTaskResult = await mediator.Send(command);

        return deleteTaskResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPatch("{taskId:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> UpdateTaskCategory(
        Guid taskId, Guid categoryId)
    {
        var command = new UpdateTaskCategoryCommand(taskId, categoryId);

        var updateTaskCategoryResult = await mediator.Send(command);

        return updateTaskCategoryResult.Match(
            _ => NoContent(),
            Problem);
    }
    [HttpGet]
    public async Task<IActionResult> ListTasks()
    {
        var command = new ListTasksQuery();

        var listTasksResult = await mediator.Send(command);

        return listTasksResult.Match(
            tasks => Ok(tasks.ConvertAll(task => new TaskResponse(task.Id, task.Name, task.Description,
                task.DueDate, task.Status.ToString(), task.CategoryName))),
            Problem);
    }
}
