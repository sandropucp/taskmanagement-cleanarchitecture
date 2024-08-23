using TaskManagement.Domain.Attachments;

namespace TaskManagement.Application.Common.Interfaces;

public interface IAttachmentsRepository
{
    Task AddAttachmentAsync(Attachment attachment);
    Task<Attachment> GetByIdAsync(Guid attachmentId);
    Task<List<Attachment>> GetAllAsync();
}