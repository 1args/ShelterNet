using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(w => w.Id);
            
        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(200);
                
        builder.Property(w => w.Latitude)
            .IsRequired()
            .HasColumnType("decimal(9,6)");
                
        builder.Property(w => w.Longitude)
            .IsRequired()
            .HasColumnType("decimal(9,6)");
                
        builder.Property(w => w.Address)
            .IsRequired()
            .HasMaxLength(500);
                
        builder.Property(w => w.Mode)
            .IsRequired()
            .HasConversion<string>();
                
        builder.Property(w => w.Capacity)
            .IsRequired();
                
        builder.Property(w => w.CreatedAt)
            .IsRequired();
                
        builder.HasMany(w => w.InventoryItems)
            .WithOne(i => i.Warehouse)
            .HasForeignKey(i => i.WarehouseId);
    }
}