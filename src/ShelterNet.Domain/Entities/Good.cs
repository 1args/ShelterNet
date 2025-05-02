namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity that represents a product type
/// </summary>
public class Good : Entity<Guid>
{
    /// <summary>Product name</summary>
    public string Name { get; set; }

    /// <summary>Product description</summary>
    public string Description { get; set; }

    /// <summary>Product priority level (0 = highest)</summary>
    public int PriorityLevel { get; set; }
    
    /// <summary>Requirements for temperature conditions</summary>
    public string TemperatureRequirements { get; set; }
    
    /// <summary>Maximum expiry date in days</summary>
    public int? ExpiryRequirementInDays { get; set; }

    public Good() { }
}