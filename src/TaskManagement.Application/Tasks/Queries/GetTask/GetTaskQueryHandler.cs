using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Queries.GetTask;

public class GetTaskQueryHandler(ITasksRepository tasksRepository) : IRequestHandler<GetTaskQuery, ErrorOr<Local.Task>>
{
    private readonly ITasksRepository tasksRepository = tasksRepository;

    public async Task<ErrorOr<Local.Task>> Handle(GetTaskQuery request,
        CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        return task is null
            ? Error.NotFound(description: "Task not found")
            : task;
    }
}
