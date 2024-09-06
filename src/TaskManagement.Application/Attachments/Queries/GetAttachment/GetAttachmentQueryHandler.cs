using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Attachments.Queries.GetAttachment;

public class GetAttachmentQueryHandler(IAttachmentsRepository attachmentsRepository) : IRequestHandler<GetAttachmentQuery, ErrorOr<Attachment>>
{
    private readonly IAttachmentsRepository attachmentsRepository = attachmentsRepository;

    public async Task<ErrorOr<Attachment>> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        var attachment = await attachmentsRepository.GetByIdAsync(request.AttachmentId);
        return attachment == null
            ? Error.NotFound(description: "Attachment not found")
            : attachment;
    }
}
