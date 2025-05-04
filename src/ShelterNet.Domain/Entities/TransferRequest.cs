using ShelterNet.Domain.Enums;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity representing a request to transfer resources between warehouses
/// </summary>
public class TransferRequest : Entity<Guid>
{
    /// <summary>Identifier of the warehouse sending the resources</summary>
    public Guid SourceWarehouseId { get; set; }

    /// <summary>Identifier of the warehouse receiving the resources</summary>
    public Guid DestinationWarehouseId { get; set; }

    /// <summary>Identifier of the resource being transferred</summary>
    public Guid ResourceId { get; set; }

    public Guid CreatorId { get; set; }

    /// <summary>Status of the transfer request</summary>
    public TransferStatus Status { get; set; }

    /// <summary>Date and time when the transfer was requested</summary>
    public DateTime RequestedAt { get; set; }

    /// <summary>Date and time when the transfer was completed (if applicable)</summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>EF navigation property for source warehouse</summary>
    public Warehouse SourceWarehouse { get; set; } = null!;

    /// <summary>EF navigation property for destination warehouse</summary>
    public Warehouse DestinationWarehouse { get; set; } = null!;

    /// <summary>EF navigation property for the resource</summary>
    public Resource Resource { get; set; } = null!;

    public TransferRequest() { }
}