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
                
        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();
                
        builder.HasMany(u => u.WarehouseAccesses)
            .WithOne(wa => wa.User)
            .HasForeignKey(wa => wa.UserId);
    }
}