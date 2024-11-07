using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionConfigurations : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.Status)
            .HasConversion(
                taskStatus => taskStatus.Value,
                value => WorkItemStatus.FromValue(value));

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
