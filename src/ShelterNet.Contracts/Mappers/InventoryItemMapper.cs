using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Contracts.Mappers;

public static class InventoryItemMapper
{
    public static InventoryItemResponse MapToPublic(this InventoryItem item) =>
        new InventoryItemResponse
        (
            WarehouseId: item.WarehouseId,
            ResourceId: item.ResourceId,
            Quantity: item.Quantity,
            ExpiryDate: item.ExpiryDate,
            BatchNumber: item.BatchNumber,
            StorageConditions: item.StorageConditions
        );
}