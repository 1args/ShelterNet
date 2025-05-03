using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.Id);
            
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);
                
        builder.Property(r => r.Description)
            .HasMaxLength(1000);
                
        builder.Property(r => r.PriorityLevel)
            .IsRequired()
            .HasConversion<string>();
                
        builder.Property(r => r.TemperatureRequirements)
            .HasMaxLength(200);
                
        builder.HasMany(r => r.InventoryItems)
            .WithOne(i => i.Resource)
            .HasForeignKey(i => i.ResourceId);
    }
}