using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Commands.UpdateTaskCategory;

public class UpdateTaskCategoryCommandHandler(ITasksRepository tasksRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateTaskCategoryCommand, ErrorOr<Local.Task>>
{
    private readonly ITasksRepository tasksRepository = tasksRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Local.Task>> Handle(
        UpdateTaskCategoryCommand request, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        if (task is null)
        {
            return Error.NotFound("description: task not found");
        }

        await tasksRepository.UpdateTaskAsync(task);
        await unitOfWork.CommitChangesAsync();

        return task;
    }
}
