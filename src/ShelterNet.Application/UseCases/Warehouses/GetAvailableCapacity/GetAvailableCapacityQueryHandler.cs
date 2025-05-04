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
        var warehouse = await warehouseRepository.SingleOrDefaultWithIncludesAsync<ICollection<InventoryItem>, Resource>(
            x => x.Id == query.WarehouseId,
            cancellationToken,
            x => x.InventoryItems, 
            item => ((InventoryItem)item).Resource 
        );
        
        if (warehouse is null)
        {
            throw new NotFoundException($"Warehouse '{query.WarehouseId}' not found.");
        }

        var usedCapacity = warehouse.InventoryItems?.Sum(i => i.Quantity) ?? 0;
        return new AvailableCapacityResponse(warehouse.Capacity - usedCapacity);
    }
}