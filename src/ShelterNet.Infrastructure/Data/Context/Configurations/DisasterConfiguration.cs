using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class DisasterConfiguration : IEntityTypeConfiguration<Disaster>
{
    public void Configure(EntityTypeBuilder<Disaster> builder)
    {
        builder.HasKey(d => d.Id);
            
        builder.Property(d => d.Type)
            .IsRequired()
            .HasConversion<string>();
                
        builder.Property(d => d.Latitude)
            .IsRequired()
            .HasColumnType("decimal(9,6)");
                
        builder.Property(d => d.Longitude)
            .IsRequired()
            .HasColumnType("decimal(9,6)");
                
        builder.Property(d => d.RadiusInKm)
            .IsRequired()
            .HasColumnType("decimal(5,2)");
                
        builder.Property(d => d.Severity)
            .IsRequired();
                
        builder.Property(d => d.StartTime)
            .IsRequired();
                
        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(1000);
    }
}