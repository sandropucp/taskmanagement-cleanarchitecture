using TaskManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Infrastructure.Users;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName);

        builder.Property(u => u.LastName);

        builder.Property(u => u.Email);

        builder.Property("_passwordHash")
            .HasColumnName("PasswordHash");
    }
}
