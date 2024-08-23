using ErrorOr;
using MediatR;
using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Attachments.Queries.GetAttachment;

public record GetAttachmentQuery(Guid AttachmentId)
    : IRequest<ErrorOr<Attachment>>;