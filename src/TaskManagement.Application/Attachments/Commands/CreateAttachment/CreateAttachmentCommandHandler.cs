using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Attachments.Commands.CreateAttachment;

public class CreateAttachmentCommandHandler(IAttachmentsRepository attachmentsRepository,
    IWorkItemsRepository workItemsRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAttachmentCommand,
    ErrorOr<Attachment>>
{
    private readonly IAttachmentsRepository attachmentsRepository = attachmentsRepository;
    private readonly IWorkItemsRepository workItemsRepository = workItemsRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Attachment>> Handle(CreateAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        var workItem = await workItemsRepository.GetByIdAsync(request.WorkItemId);

        if (workItem is null)
        {
            return Error.NotFound(description: "task not found");
        }

        var attachment = new Attachment(
            fileName: request.FileName,
            workItemId: workItem.Id);

        var addAttachmentResult = workItem.AddAttachment(attachment);

        if (addAttachmentResult.IsError)
        {
            return addAttachmentResult.Errors;
        }

        await workItemsRepository.UpdateWorkItemAsync(workItem);               //Task with the attachment
        await attachmentsRepository.AddAttachmentAsync(attachment);//Add the attachment
        await unitOfWork.CommitChangesAsync();

        return attachment;
    }
}
