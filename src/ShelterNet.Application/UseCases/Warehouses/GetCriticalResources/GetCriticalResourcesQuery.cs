using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;

public sealed record GetCriticalResourcesQuery(
    Guid WarehouseId) : IQuery<IEnumerable<InventoryItem>>;