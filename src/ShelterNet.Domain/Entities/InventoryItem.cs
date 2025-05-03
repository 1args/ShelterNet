namespace ShelterNet.Domain.Entities;

/// <summary>
/// Presentation of goods stored in a specific warehouse
/// </summary>
public class InventoryItem : Entity<Guid>
{
    /// <summary>Warehouse identifier</summary>
    public Guid WarehouseId { get; set; }

    /// <summary>Item identifier</summary>
    public Guid ResourceId { get; set; }
    
    /// <summary>Number of units of goods</summary>
    public int Quantity { get; set; }
    
    /// <summary>Item expiry date (if any)</summary>
    public DateTime? ExpiryDate { get; set; }
    
    /// <summary>Item batch number</summary>
    public string BatchNumber { get; set; }
    
    /// <summary>Storage conditions of the goods</summary>
    public string StorageConditions { get; set; }

    /// <summary>Date the record was last updated</summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>EF navigation property for the warehouse</summary>
    public Warehouse Warehouse { get; set; } = null!;
    
    /// <summary>EF navigation property for the resourse</summary>
    public Resource Resource { get; set; } = null!;

    public InventoryItem() { }
}