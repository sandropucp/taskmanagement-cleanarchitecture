using ErrorOr;
using MediatR;
using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Attachments.Commands.CreateAttachment;

public record CreateAttachmentCommand(string FileName, Guid TaskId) : IRequest<ErrorOr<Attachment>>;