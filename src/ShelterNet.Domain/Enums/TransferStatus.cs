namespace ShelterNet.Domain.Enums;

/// <summary>
/// Current status of a transfer request
/// </summary>
public enum TransferStatus
{
    /// <summary>Request is pending approval</summary>
    Pending = 0,

    /// <summary>Request has been approved</summary>
    Approved = 1,

    /// <summary>Request has been rejected</summary>
    Rejected = 2,

    /// <summary>Request has been completed</summary>
    Completed = 3,

    /// <summary>Request has been cancelled</summary>
    Cancelled = 4
}