using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Comments.Persistence;
public class CommentsRepository : ICommentsRepository
{
    private readonly TaskManagementDbContext _dbContext;

    public CommentsRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }
}