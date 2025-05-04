using Microsoft.EntityFrameworkCore;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Infrastructure.Data.Context;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<InventoryItem> InventoryItems { get; set; }
    
    public DbSet<Resource> Resources { get; set; }
    
    public DbSet<Disaster> Disasters { get; set; }
    
    public DbSet<TransferRequest> TransferRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CustomModelBuilder.OnModelCreating(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}