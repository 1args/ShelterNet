namespace ShelterNet.Domain.Enums;

/// <summary>
/// Role of a user in the system
/// </summary>
public enum Role
{
    /// <summary>Analyst with access to reports and analytics</summary>
    Analyst = 1,

    /// <summary>Manager of a specific warehouse</summary>
    WarehouseManager = 2,

    /// <summary>Member of central management with full system access</summary>
    CentralManagement = 3,
}