namespace ShelterNet.Domain.Enums;

/// <summary>
/// Enumeration of system permissions
/// </summary>
public enum Permission
{
    /// <summary>Basic access permission</summary>
    Access = 1,

    /// <summary>Create permission</summary>
    Create = 2,

    /// <summary>Read permission</summary>
    Read = 3,

    /// <summary>Update permission</summary>
    Update = 4,

    /// <summary>Delete permission</summary>
    Delete = 5,
}