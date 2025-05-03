namespace ShelterNet.Domain.Enums;

/// <summary>
/// An enumeration that represents the states of a warehouse
/// </summary>
public enum OperationalMode
{
    /// <summary>Inactive (unavailable or out of service)</summary>
    Inactive = 0, 
    
    /// <summary>Operates in the normal mode</summary>
    Normal = 1,  
    
    /// <summary>Emergency mode (in case of a disaster)</summary>
    Emergency = 2 
}