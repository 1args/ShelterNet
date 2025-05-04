namespace ShelterNet.Domain.Entities;

/// <summary>
/// Linking table for user-role many-to-many relationship
/// </summary>
public class UserRole
{
    /// <summary>User identifier</summary>
    public Guid UserId { get; set; }
    
    /// <summary>Role identifier</summary>
    public int RoleId { get; set; }
}