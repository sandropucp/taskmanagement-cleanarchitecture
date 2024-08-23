using ErrorOr;
using MediatR;
using TaskManagement.Domain.Attachments;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Attachments.Queries.GetAttachment;

public class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, ErrorOr<Attachment>>
{
    private readonly IAttachmentsRepository _attachmentsRepository;

    public GetAttachmentQueryHandler(IAttachmentsRepository attachmentsRepository)
    {
        _attachmentsRepository = attachmentsRepository;
    }

    public async Task<ErrorOr<Attachment>> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentsRepository.GetByIdAsync(request.AttachmentId);
        return attachment == null
            ? Error.NotFound(description: "Attachment not found")
            : attachment;
    }
}