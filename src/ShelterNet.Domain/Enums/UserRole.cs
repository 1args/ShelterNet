namespace ShelterNet.Domain.Enums;

/// <summary>
/// Role of a user in the system
/// </summary>
public enum UserRole
{
    /// <summary>Analyst with access to reports and analytics</summary>
    Analyst = 0,

    /// <summary>Manager of a specific warehouse</summary>
    WarehouseManager = 1,

    /// <summary>Member of central management with full system access</summary>
    CentralManagement = 2,
}