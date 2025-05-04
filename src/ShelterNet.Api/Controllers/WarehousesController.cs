using Microsoft.AspNetCore.Mvc;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Application.UseCases.Warehouses.GetAvailableCapacity;
using ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;
using ShelterNet.Application.UseCases.Warehouses.UpdateWarehouseStatus;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Api.Controllers;

[ApiController]
[Route("/api/warehouses")]
public class WarehousesController(
    ICommandHandler<UpdateWarehouseStatusCommand> updateWarehouseHandler,
    IQueryHandler<GetCriticalResourcesQuery, IEnumerable<InventoryItem>> getCriticalResourcesHandler,
    IQueryHandler<GetAvailableCapacityQuery, AvailableCapacityResponse> getAvailableCapacityHandler) : ControllerBase
{
    [HttpPatch("update")]
    public async Task<ActionResult> UpdateWarehouseStatusAsync(
        [FromBody] UpdateWarehouseStatusCommand warehouseCommand,
        CancellationToken cancellationToken)
    {
        await updateWarehouseHandler.HandleAsync(warehouseCommand, cancellationToken);
        return Ok();
    }
    
    [HttpGet("resources")]
    public async Task<ActionResult> GetCriticalResourcesAsync(
        [FromRoute] GetCriticalResourcesQuery warehouseCommand,
        CancellationToken cancellationToken)
    {
        var data = await getCriticalResourcesHandler.HandleAsync(warehouseCommand, cancellationToken);
        return Ok(data);
    }
    
    [HttpGet("capacity")]
    public async Task<ActionResult> GetAvailableCapacityAsync(
        [FromRoute] GetAvailableCapacityQuery warehouseCommand,
        CancellationToken cancellationToken)
    {
        var data = await getAvailableCapacityHandler.HandleAsync(warehouseCommand, cancellationToken);
        return Ok(data);
    }
}