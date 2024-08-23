using ErrorOr;
using MediatR;
using TaskManagement.Domain.Attachments;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Attachments.Commands.CreateAttachment;

public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand,
    ErrorOr<Attachment>>
{
    private readonly IAttachmentsRepository _attachmentsRepository;
    private readonly ITasksRepository _tasksRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAttachmentCommandHandler(IAttachmentsRepository attachmentsRepository,
        ITasksRepository tasksRepository, IUnitOfWork unitOfWork)
    {
        _attachmentsRepository = attachmentsRepository;
        _tasksRepository = tasksRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Attachment>> Handle(CreateAttachmentCommand command,
        CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetByIdAsync(command.TaskId);

        if (task is null)
        {
            return Error.NotFound(description: "task not found");
        }

        var attachment = new Attachment(
            fileName: command.FileName,
            taskId: task.Id);

        var addAttachmentResult = task.AddAttachment(attachment);

        if (addAttachmentResult.IsError)
        {
            return addAttachmentResult.Errors;
        }

        await _tasksRepository.UpdateTaskAsync(task);               //Task with the attachment
        await _attachmentsRepository.AddAttachmentAsync(attachment);//Add the attachment
        await _unitOfWork.CommitChangesAsync();

        return attachment;
    }
}