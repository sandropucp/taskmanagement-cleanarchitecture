using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Attachments.Commands.CreateAttachment;

public class CreateAttachmentCommandHandler(IAttachmentsRepository attachmentsRepository,
    ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAttachmentCommand,
    ErrorOr<Attachment>>
{
    private readonly IAttachmentsRepository _attachmentsRepository = attachmentsRepository;
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Attachment>> Handle(CreateAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetByIdAsync(request.TaskId);

        if (task is null)
        {
            return Error.NotFound(description: "task not found");
        }

        var attachment = new Attachment(
            fileName: request.FileName,
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
