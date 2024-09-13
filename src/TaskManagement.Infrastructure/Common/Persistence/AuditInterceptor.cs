using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly List<AuditEntry> auditEntriesList;
    public AuditInterceptor() => auditEntriesList = [];
    public AuditInterceptor(List<AuditEntry> auditEntries) => auditEntriesList = auditEntries;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        var startTime = DateTime.UtcNow;
        var entries = eventData.Context.ChangeTracker
            .Entries()
            .Where(entry => entry.Entity is not AuditEntry
                &&
                entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(entry => new AuditEntry
            {
                Id = Guid.NewGuid(),
                Metadata = entry.DebugView.LongView,
                StartTimeUtc = startTime,
                Succeded = true,
                ErrorMessage = string.Empty
            }).ToList();
        if (entries.Count == 0)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        auditEntriesList.AddRange(entries);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (auditEntriesList is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        var endTime = DateTime.UtcNow;
        foreach (var entry in auditEntriesList)
        {
            entry.EndTimeUtc = endTime;
            entry.Succeded = true;
        }
        if (auditEntriesList.Count > 0 && eventData.Context is not null)
        {
            // Save audit entries to the database
            // Better approah is to write audit entries to a message bus and let another service handle the audit entries
            eventData.Context.Set<AuditEntry>().AddRange(auditEntriesList);
            auditEntriesList.Clear();
            await eventData.Context.SaveChangesAsync(cancellationToken);
        }
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    public override async void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (auditEntriesList is null)
        {
            return;
        }
        var endTime = DateTime.UtcNow;
        foreach (var entry in auditEntriesList)
        {
            entry.EndTimeUtc = endTime;
            entry.Succeded = false;
            entry.ErrorMessage = eventData.Exception.Message;
        }
        if (auditEntriesList.Count > 0 && eventData.Context is not null)
        {
            // Save audit entries to the database
            // Better approah is to write audit entries to a message bus and let another service handle the audit entries
            eventData.Context.Set<AuditEntry>().AddRange(auditEntriesList);
            auditEntriesList.Clear();
            await eventData.Context.SaveChangesAsync();
        }
        return;
    }
}