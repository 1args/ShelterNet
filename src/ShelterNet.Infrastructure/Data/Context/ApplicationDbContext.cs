using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShelterNet.Domain.Entities;
using ShelterNet.Infrastructure.Data.Context.Configurations;
using ShelterNet.Infrastructure.Options;

namespace ShelterNet.Infrastructure.Data.Context;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IOptions<AuthorizationOptions> authorizationOptions) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<InventoryItem> InventoryItems { get; set; }
    
    public DbSet<Resource> Resources { get; set; }
    
    public DbSet<Disaster> Disasters { get; set; }
    
    public DbSet<TransferRequest> TransferRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CustomModelBuilder.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authorizationOptions.Value));
        base.OnModelCreating(modelBuilder);
    }
}