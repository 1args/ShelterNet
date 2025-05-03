using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class TransferRequestConfiguration : IEntityTypeConfiguration<TransferRequest>
{
    public void Configure(EntityTypeBuilder<TransferRequest> builder)
    {
        builder.HasKey(t => t.Id);
            
        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();
                
        builder.Property(t => t.RequestedAt)
            .IsRequired();
                
        builder.HasOne(t => t.SourceWarehouse)
            .WithMany()
            .HasForeignKey(t => t.SourceWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(t => t.DestinationWarehouse)
            .WithMany()
            .HasForeignKey(t => t.DestinationWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(t => t.Resource)
            .WithMany()
            .HasForeignKey(t => t.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}