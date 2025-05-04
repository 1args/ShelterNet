using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Application.Exceptions;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Warehouses.GetAvailableCapacity;

public class GetAvailableCapacityQueryHandler(
    IRepository<Warehouse> warehouseRepository) : IQueryHandler<GetAvailableCapacityQuery, AvailableCapacityResponse>
{
    public async Task<AvailableCapacityResponse> HandleAsync(GetAvailableCapacityQuery query, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.SingleOrDefaultAsync(
            w => w.Id == query.WarehouseId,
            cancellationToken,
            true,
            w => w.InventoryItems);
        
        if (warehouse is null)
        {
            throw new NotFoundException($"Warehouse '{query.WarehouseId}' not found.");
        }

        var usedCapacity = warehouse.InventoryItems.Sum(i => i.Quantity);
        return new AvailableCapacityResponse(warehouse.Capacity - usedCapacity);
    }
}