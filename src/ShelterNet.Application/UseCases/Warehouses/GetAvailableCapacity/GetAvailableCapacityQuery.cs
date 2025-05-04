using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Contracts.ApiContracts.Responses;

namespace ShelterNet.Application.UseCases.Warehouses.GetAvailableCapacity;

public record GetAvailableCapacityQuery(
    Guid WarehouseId) : IQuery<AvailableCapacityResponse>;