using Microsoft.AspNetCore.Mvc;
using ShelterNet.Api.Authorization;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Application.UseCases.Warehouses.GetAvailableCapacity;
using ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;
using ShelterNet.Application.UseCases.Warehouses.UpdateWarehouseStatus;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;
using Permission = ShelterNet.Domain.Enums.Permission;

namespace ShelterNet.Api.Controllers;

[ApiController]
[Route("/api/warehouses")]
public class WarehousesController(
    ICommandHandler<UpdateWarehouseStatusCommand> updateWarehouseHandler,
    IQueryHandler<GetCriticalResourcesQuery, IEnumerable<InventoryItemResponse>> getCriticalResourcesHandler,
    IQueryHandler<GetAvailableCapacityQuery, AvailableCapacityResponse> getAvailableCapacityHandler) : ControllerBase
{
    [HttpPatch("/update")]
    [HasPermission(Permission.Access)]
    public async Task<ActionResult> UpdateWarehouseStatusAsync(
        [FromBody] UpdateWarehouseStatusCommand warehouseCommand,
        CancellationToken cancellationToken)
    {
        await updateWarehouseHandler.HandleAsync(warehouseCommand, cancellationToken);
        return Ok();
    }
    
    [HttpGet("{id:guid}/resources")]
    [HasPermission(Permission.Read)]
    public async Task<ActionResult> GetCriticalResourcesAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var data = await getCriticalResourcesHandler.HandleAsync(
            new GetCriticalResourcesQuery(id), cancellationToken);
        return Ok(data);
    }
    
    [HttpGet("{id:guid}/capacity")]
    [HasPermission(Permission.Read)]
    public async Task<ActionResult> GetAvailableCapacityAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var data = await getAvailableCapacityHandler.HandleAsync(
            new GetAvailableCapacityQuery(id), cancellationToken);
        return Ok(data);
    }
}