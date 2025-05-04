using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;

public sealed class GetCriticalResourcesQueryHandler(
    IRepository<Warehouse> warehouseRepository) : IQueryHandler<GetCriticalResourcesQuery, IEnumerable<InventoryItem>>
{
    public async Task<IEnumerable<InventoryItem>> HandleAsync(GetCriticalResourcesQuery query, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.SingleOrDefaultAsync(
            w => w.Id == query.WarehouseId,
            cancellationToken,
            true,
            w => w.InventoryItems!);
        
        if (warehouse is null)
        {
            throw new NotFoundException($"Warehouse '{query.WarehouseId}' not found.");
        }

        return warehouse.InventoryItems
            .OrderBy(i => i.Resource.PriorityLevel)
            .ThenBy(i => i.ExpiryDate)
            .ToList();
    }
}