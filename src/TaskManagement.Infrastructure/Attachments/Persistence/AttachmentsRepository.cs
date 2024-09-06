using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Attachments.Persistence;
public class AttachmentsRepository(TaskManagementDbContext dbContext) : IAttachmentsRepository
{
    private readonly TaskManagementDbContext _dbContext = dbContext;

    public Task AddAttachmentAsync(Attachment attachment) => throw new NotImplementedException();

    public async Task<Attachment> CreateAttachmentAsync(Attachment attachment)
    {
        await _dbContext.Attachments.AddAsync(attachment);
        await _dbContext.SaveChangesAsync();
        return attachment;
    }

    public Task<List<Attachment>> GetAllAsync() => throw new NotImplementedException();

    public Task<Attachment> GetAttachmentByIdAsync(Guid attachmentId) => throw new NotImplementedException();

    public Task<Attachment> GetByIdAsync(Guid attachmentId) => throw new NotImplementedException();
}
