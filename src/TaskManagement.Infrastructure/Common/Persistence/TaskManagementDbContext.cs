using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;
using TaskManagement.Domain.Categories;
using TaskManagement.Domain.Comments;
using TaskManagement.Domain.Users;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class TaskManagementDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    private readonly List<AuditEntry> auditEntriesList = [];
    public DbSet<Local.Task> Tasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Attachment> Attachments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<AuditEntry> AuditEntries { get; set; } = null!;
    public async Task CommitChangesAsync() => await SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor(auditEntriesList));
        base.OnConfiguring(optionsBuilder);
    }
}
