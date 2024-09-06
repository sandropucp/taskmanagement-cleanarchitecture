using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Comments.Persistence;
public class CommentsRepository(TaskManagementDbContext dbContext) : ICommentsRepository
{
    private readonly TaskManagementDbContext dbContext = dbContext;

    public async Task AddCommentAsync(Comment comment)
    {
        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid commentId) => await dbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == commentId);

    public async Task<List<Comment>> GetAllAsync() => await dbContext.Comments.ToListAsync();
}
