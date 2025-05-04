using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;

public sealed record GetCriticalResourcesQuery(
    Guid WarehouseId) : IQuery<IEnumerable<InventoryItemResponse>>;