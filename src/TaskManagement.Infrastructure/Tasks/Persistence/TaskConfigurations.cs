using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionConfigurations : IEntityTypeConfiguration<Local.Task>
{
    public void Configure(EntityTypeBuilder<Local.Task> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.Status)
            .HasConversion(
                taskStatus => taskStatus.Value,
                value => Local.TaskStatus.FromValue(value));

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
