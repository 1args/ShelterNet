using ShelterNet.Domain.Enums;
using ShelterNet.Domain.Primitives;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity representing an emergency (disaster)
/// </summary>
public class Disaster : Entity<Guid>
{
    /// <summary>Type of disaster</summary>
    public DisasterType Type { get; set; }
    
    /// <summary>Geolocation of the epicentre (latitude)</summary>
    public decimal Latitude { get; set; }
    
    /// <summary>Geolocation of the epicentre (longitude)</summary>
    public decimal Longitude { get; set; }
    
    /// <summary>Type</summary>
    public decimal RadiusInKm { get; set; }
    
    /// <summary>Type</summary>
    public int Severity { get; set; }
    
    /// <summary>Type</summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>Type</summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>Type</summary>
    public string Description { get; set; }

    public Disaster() { }
}