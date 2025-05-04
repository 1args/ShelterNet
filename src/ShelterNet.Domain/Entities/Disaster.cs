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
    /// <summary>Radius of the affected area in kilometers</summary>
    public decimal RadiusInKm { get; set; }
    
    /// <summary>Severity of the disaster (e.g., scale from 1 to 10)</summary>
    public int Severity { get; set; }
    
    /// <summary>Date and time when the disaster started</summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>Date and time when the disaster ended (if known)</summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>Detailed description of the disaster</summary>
    public string Description { get; set; }gia

    public Disaster() { }
}