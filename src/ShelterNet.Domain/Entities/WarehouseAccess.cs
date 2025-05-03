using ShelterNet.Domain.Enums;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity representing access permissions of a user to a warehouse
/// </summary>
public class WarehouseAccess : Entity<Guid>
{
    /// <summary>User identifier</summary>
    public Guid UserId { get; set; }

    /// <summary>Warehouse identifier</summary>
    public Guid WarehouseId { get; set; }

    /// <summary>Level of access granted</summary>
    public AccessLevel AccessLevel { get; set; }

    /// <summary>EF navigation property for the user</summary>
    public User User { get; set; } = null!;

    /// <summary>EF navigation property for the warehouse</summary>
    public Warehouse Warehouse { get; set; } = null!;

    public WarehouseAccess() { }
}