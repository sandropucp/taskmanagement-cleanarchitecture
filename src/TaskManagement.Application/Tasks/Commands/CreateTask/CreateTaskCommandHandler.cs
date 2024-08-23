using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler :
    IRequestHandler<CreateTaskCommand, ErrorOr<Local.Task>>
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(ITasksRepository tasksRepository,
        IUnitOfWork unitOfWork)
    {
        _tasksRepository = tasksRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Local.Task>> Handle(
        CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new Local.Task(
            name: request.Name, description: request.Description,
            dueDate: request.DueDate, taskStatus: request.TaskStatus,
            categoryId: request.CategoryId,
            assignedToId: request.UserId);

        await _tasksRepository.AddTaskAsync(task);
        await _unitOfWork.CommitChangesAsync();

        return task;
    }
}
