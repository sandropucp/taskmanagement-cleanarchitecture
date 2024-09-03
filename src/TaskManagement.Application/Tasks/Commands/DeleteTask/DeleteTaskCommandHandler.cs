using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ErrorOr<Deleted>>
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ITasksRepository tasksRepository)
    {
        _unitOfWork = unitOfWork;
        _tasksRepository = tasksRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetByIdAsync(request.TaskId);

        if (task == null)
        {
            return Error.NotFound("description: task not found");
        }

        await _tasksRepository.RemoveTaskAsync(task);
        await _unitOfWork.CommitChangesAsync();

        return new Deleted();
    }
}