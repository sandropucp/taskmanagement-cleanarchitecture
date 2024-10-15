using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Tasks;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler(ITasksRepository tasksRepository,
    ICategoriesRepository categoriesRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<CreateTaskCommand, ErrorOr<Local.Task>>
{
    public async Task<ErrorOr<Local.Task>> Handle(
        CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);
        var assignedUser = await usersRepository.GetByIdAsync(request.UserAssignedToId);

        if (category is null)
        {
            return TaskErrors.CategoryNotFound;
        }
        if (assignedUser is null)
        {
            return TaskErrors.AssignedUserNotFound;
        }

        var task = new Local.Task(
            name: request.Name, description: request.Description,
            dueDate: request.DueDate, taskStatus: request.TaskStatus,
            categoryId: request.CategoryId,
            categoryName: category.Name,
            assignedToId: request.UserAssignedToId,
            assignedToName: assignedUser.Name);

        await tasksRepository.AddTaskAsync(task);
        await unitOfWork.CommitChangesAsync();

        return task;
    }
}
