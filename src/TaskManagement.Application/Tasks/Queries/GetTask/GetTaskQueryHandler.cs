using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Queries.GetTask;

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, ErrorOr<Local.Task>>
{
    private readonly ITasksRepository _tasksRepository;

    public GetTaskQueryHandler(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<ErrorOr<Local.Task>> Handle(GetTaskQuery query,
        CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetByIdAsync(query.TaskId);
        return task is null
            ? Error.NotFound(description: "Task not found")
            : task;
    }
}
