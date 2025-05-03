using ShelterNet.Domain.Enums;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity representing a system user
/// </summary>
public class User : Entity<Guid>
{
    /// <summary>User's full name</summary>
    public string FullName { get; set; }

    /// <summary>User's email address</summary>
    public string Email { get; set; }

    /// <summary>Hashed password</summary>
    public string PasswordHash { get; set; }

    /// <summary>User role in the system</summary>
    public UserRole Role { get; set; }

    /// <summary>EF navigation property for the user's warehouse access permissions</summary>
    public ICollection<WarehouseAccess> WarehouseAccesses { get; set; }

    public User() { }
}