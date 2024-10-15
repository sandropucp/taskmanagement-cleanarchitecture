using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;
using TaskManagement.Domain.Categories;
using TaskManagement.Domain.Comments;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Admins;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class TaskManagementDbContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor) : DbContext(options), IUnitOfWork
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly List<AuditEntry> auditEntriesList = [];
    public DbSet<Local.Task> Tasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Attachment> Attachments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<AuditEntry> AuditEntries { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        // get hold of all the domain events
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();

        AddDomainEventsToOfflineProcessingQueue(domainEvents);

        // // store them in the http context for later if user is waiting online
        // if (IsUserWaitingOnline())
        // {
        //     AddDomainEventsToOfflineProcessingQueue(domainEvents);
        // }
        // else
        // {
        //     await PublishDomainEvents(_publisher, domainEvents);
        // }

        await SaveChangesAsync();
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // fetch queue from http context or create a new queue if it doesn't exist
        var domainEventsQueue = httpContextAccessor.HttpContext!.Items
            .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new Queue<IDomainEvent>();

        // add the domain events to the end of the queue
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // store the queue in the http context
        httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
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
