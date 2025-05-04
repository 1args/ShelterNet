using Microsoft.EntityFrameworkCore;
using ShelterNet.Infrastructure.Data.Context.Configurations;

namespace ShelterNet.Infrastructure.Data.Context;

/// <summary>
/// Static class responsible for applying entity configurations during model creation
/// </summary>
public static class CustomModelBuilder
{
    /// <summary>
    /// Applies configuration classes for each entity using the Fluent API
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure entity mappings</param>
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DisasterConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryItemConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new TransferRequestConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
    }
}