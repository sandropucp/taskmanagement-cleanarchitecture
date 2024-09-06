using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Attachments.Commands.CreateAttachment;

public class CreateAttachmentCommandHandler(IAttachmentsRepository attachmentsRepository,
    ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAttachmentCommand,
    ErrorOr<Attachment>>
{
    private readonly IAttachmentsRepository attachmentsRepository = attachmentsRepository;
    private readonly ITasksRepository tasksRepository = tasksRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Attachment>> Handle(CreateAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);

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

        await tasksRepository.UpdateTaskAsync(task);               //Task with the attachment
        await attachmentsRepository.AddAttachmentAsync(attachment);//Add the attachment
        await unitOfWork.CommitChangesAsync();

        return attachment;
    }
}
