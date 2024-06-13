using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Local = TaskManagement.Domain.Tasks;
using TaskManagement.Infrastructure.Common.Persistence;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionConfigurations : IEntityTypeConfiguration<Local.Task>
{
    public void Configure(EntityTypeBuilder<Local.Task> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
