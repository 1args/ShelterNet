using ShelterNet.Domain.Primitives;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// System permission entity
/// </summary>
public class Permission : Entity<int>
{
    /// <summary>Name of the permission</summary>
    public string Name { get; set; }

    /// <summary>Collection of roles that have this permission</summary>
    public ICollection<Role> Roles { get; set; }

    public Permission() { }
}