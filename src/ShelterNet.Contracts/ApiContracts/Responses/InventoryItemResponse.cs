namespace ShelterNet.Contracts.ApiContracts.Responses;

public record InventoryItemResponse(
    Guid WarehouseId,
    Guid ResourceId,
    int Quantity,
    DateTime? ExpiryDate,
    string BatchNumber,
    string StorageConditions);
