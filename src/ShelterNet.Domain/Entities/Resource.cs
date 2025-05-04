using ShelterNet.Domain.Enums;
using ShelterNet.Domain.Primitives;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity that represents a resource type
/// </summary>
public class Resource : Entity<Guid>
{
    /// <summary>Resource name</summary>
    public string Name { get; set; }

    /// <summary>Resource description</summary>
    public string Description { get; set; }

    /// <summary>Resource priority level (0 = highest)</summary>
    public ResourcePriority PriorityLevel { get; set; }
    
    /// <summary>Requirements for temperature conditions</summary>
    public string TemperatureRequirements { get; set; }
    
    /// <summary>Maximum expiry date in days</summary>
    public int? ExpiryRequirementInDays { get; set; }

    /// <summary> List of inventory items</summary>
    public ICollection<InventoryItem> InventoryItems { get; set; }

    public Resource() { }
}