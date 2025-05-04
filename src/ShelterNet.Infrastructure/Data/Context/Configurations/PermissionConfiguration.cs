using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasMany(p => p.Roles)
            .WithMany(r => r.Permissions)
            .UsingEntity<RolePermission>(
                left => left.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId),
                right => right.HasOne<Permission>().WithMany().HasForeignKey(p => p.PermissionId)
            );

        var permissions = Enum
            .GetValues<Domain.Enums.Permission>()
            .Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(permissions);
    }
}