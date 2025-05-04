using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
            
        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(200);
                
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(320);
                
        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(l => l.UserId));
    }
}