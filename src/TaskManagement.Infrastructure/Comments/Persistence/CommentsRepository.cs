using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Comments.Persistence;
public class CommentsRepository(TaskManagementDbContext dbContext) : ICommentsRepository
{
    private readonly TaskManagementDbContext _dbContext = dbContext;

    public async Task AddCommentAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid commentId) => await _dbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == commentId);

    public async Task<List<Comment>> GetAllAsync() => await _dbContext.Comments.ToListAsync();
}
