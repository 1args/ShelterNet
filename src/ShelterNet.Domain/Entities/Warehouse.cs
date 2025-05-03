using ShelterNet.Domain.Enums;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity that represents a warehouse
/// </summary>
public class Warehouse : Entity<Guid>
{
    /// <summary>Warehouse name</summary>
    public string Name { get; set; }
    
    /// <summary>Warehouse geolocation (latitude)</summary>
    public decimal Latitude { get; set; }
    
    /// <summary>Warehouse geolocation (longitude)</summary>
    public decimal Longitude { get; set; }
    
    /// <summary>Physical address of the warehouse</summary>
    public string Address { get; set; }

    /// <summary>Current warehouse operating mode</summary>
    public OperationalMode Mode { get; set; }

    /// <summary>Maximum warehouse capacity (in the number of units of goods)</summary>
    public int Capacity { get; set; }
    
    /// <summary>Date of creation of the warehouse</summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>Date the record was last updated</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>List of resources stored in the warehouse</summary>
    public ICollection<InventoryItem> InventoryItems { get; set; }
    
    public Warehouse() { }
}