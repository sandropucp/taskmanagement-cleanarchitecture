using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.WorkItems.Commands.CreateWorkItem;
using TaskManagement.Application.WorkItems.Commands.DeleteWorkItem;
using TaskManagement.Application.WorkItems.Commands.UpdateWorkItemCategory;
using TaskManagement.Application.WorkItems.Queries.GetWorkItem;
using TaskManagement.Application.WorkItems.Queries.ListWorkItems;
using TaskManagement.Contracts.WorkItems;
using DomainWorkItemStatus = TaskManagement.Domain.WorkItems.WorkItemStatus;

namespace TaskManagement.Api.Controllers;

[Route("tasks")]
public class TasksController(ISender mediator) : ApiController
{
    private readonly ISender mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateWorkItemRequest request)
    {
        if (!DomainWorkItemStatus.TryFromName(
            request.WorkItemStatus.ToString(),
            out var taskStatus))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid task status");
        }

        var command = new CreateWorkItemCommand(request.Name, request.Description,
            request.DueDate, taskStatus, request.CategoryId, request.UserAssignedToId);

        var createTaskResult = await mediator.Send(command);
        return createTaskResult.MatchFirst(
            workItem => Ok(new WorkItemResponse(workItem.Id, workItem.Name, workItem.Description,
                workItem.DueDate, workItem.Status.ToString(), workItem.CategoryName)),
            Problem);
    }

    [HttpGet("{workItemId:guid}")]
    public async Task<IActionResult> GetTask(Guid workItemId)
    {
        var query = new GetWorkItemQuery(workItemId);

        var getWorkItemResult = await mediator.Send(query);

        return getWorkItemResult.Match(
            workItem => Ok(new WorkItemResponse(
                workItem.Id,
                workItem.Name, workItem.Description, workItem.DueDate, workItem.Status.ToString(), workItem.CategoryName)),
            Problem);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteWorkItem(Guid taskId)
    {
        var command = new DeleteWorkItemCommand(taskId);

        var deleteWorkItemResult = await mediator.Send(command);

        return deleteWorkItemResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPatch("{taskId:guid}/category/{categoryId:guid}")]
    public async Task<IActionResult> UpdateWorkItemCategory(
        Guid taskId, Guid categoryId)
    {
        var command = new UpdateWorkItemCategoryCommand(taskId, categoryId);

        var updateWorkItemCategoryResult = await mediator.Send(command);

        return updateWorkItemCategoryResult.Match(
            _ => NoContent(),
            Problem);
    }
    [HttpGet]
    public async Task<IActionResult> ListWorkItems()
    {
        var command = new ListWorkItemsQuery();

        var listTasksResult = await mediator.Send(command);

        return listTasksResult.Match(
            workItems => Ok(workItems.ConvertAll(workItem => new WorkItemResponse(workItem.Id, workItem.Name, workItem.Description,
                workItem.DueDate, workItem.Status.ToString(), workItem.CategoryName))),
            Problem);
    }
}
