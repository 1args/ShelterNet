using ShelterNet.Domain.Primitives;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// System role entity
/// </summary>
public class Role : Entity<int>
{
    /// <summary>Name of the role</summary>
    public string Name { get; set; }

    /// <summary>Collection of permissions assigned to the role</summary>
    public ICollection<Permission> Permissions { get; set; }
    
    /// <summary>Collection of users who have this role</summary>
    public ICollection<User> Users { get; set; }

    public Role() { }
}