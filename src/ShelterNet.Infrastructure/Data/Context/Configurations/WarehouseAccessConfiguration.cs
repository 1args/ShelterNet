using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class WarehouseAccessConfiguration : IEntityTypeConfiguration<WarehouseAccess>
{
    public void Configure(EntityTypeBuilder<WarehouseAccess> builder)
    {
        builder.HasKey(wa => wa.Id);
            
        builder.Property(wa => wa.AccessLevel)
            .IsRequired()
            .HasConversion<string>();
                
        builder.HasOne(wa => wa.User)
            .WithMany(u => u.WarehouseAccesses)
            .HasForeignKey(wa => wa.UserId)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(wa => wa.Warehouse)
            .WithMany()
            .HasForeignKey(wa => wa.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(wa => new { wa.UserId, wa.WarehouseId })
            .IsUnique();
    }
}