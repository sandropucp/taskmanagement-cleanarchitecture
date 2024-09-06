using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler(
    IUnitOfWork unitOfWork,
    ITasksRepository tasksRepository) : IRequestHandler<DeleteTaskCommand, ErrorOr<Deleted>>
{
    private readonly ITasksRepository tasksRepository = tasksRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);

        if (task == null)
        {
            return Error.NotFound("description: task not found");
        }

        await tasksRepository.RemoveTaskAsync(task);
        await unitOfWork.CommitChangesAsync();

        return new Deleted();
    }
}
