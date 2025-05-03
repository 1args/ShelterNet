namespace ShelterNet.Domain.Enums;

/// <summary>
/// Access level of a user to a specific warehouse
/// </summary>
public enum AccessLevel
{
    /// <summary>Read-only access to view warehouse data</summary>
    ReadOnly = 0,
    
    /// <summary>Full access to manage warehouse operations</summary>
    FullAccess = 1,
}