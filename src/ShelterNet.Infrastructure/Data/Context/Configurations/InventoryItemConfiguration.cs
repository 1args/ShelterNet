using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.HasKey(i => i.Id);
            
        builder.Property(i => i.Quantity)
            .IsRequired();
                
        builder.Property(i => i.BatchNumber)
            .HasMaxLength(100);
                
        builder.Property(i => i.StorageConditions)
            .HasMaxLength(500);
                
        builder.HasOne(i => i.Warehouse)
            .WithMany(w => w.InventoryItems)
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(i => i.Resource)
            .WithMany(r => r.InventoryItems)
            .HasForeignKey(i => i.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}