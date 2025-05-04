using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Application.Exceptions;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Contracts.Mappers;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;

public sealed class GetCriticalResourcesQueryHandler(
    IRepository<Warehouse> warehouseRepository) : IQueryHandler<GetCriticalResourcesQuery, IEnumerable<InventoryItemResponse>>
{
    public async Task<IEnumerable<InventoryItemResponse>> HandleAsync(GetCriticalResourcesQuery query, CancellationToken cancellationToken)
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

        return warehouse.InventoryItems
            .Where(i => i.Resource != null)
            .OrderBy(i => i.Resource!.PriorityLevel)
            .ThenBy(i => i.ExpiryDate)
            .Select(i => i.MapToPublic()) // Використовуємо ваш маппер
            .ToList();
    }
}