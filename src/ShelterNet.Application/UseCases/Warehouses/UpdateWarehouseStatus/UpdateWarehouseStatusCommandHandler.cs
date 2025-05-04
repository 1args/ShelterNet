using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Warehouses.UpdateWarehouseStatus;

public sealed class UpdateWarehouseStatusCommandHandler(
    IRepository<Warehouse> warehouseRepository,
    ILogger<UpdateWarehouseStatusCommandHandler> logger) : ICommandHandler<UpdateWarehouseStatusCommand>
{
    public async Task HandleAsync(UpdateWarehouseStatusCommand command, CancellationToken cancellationToken)
    {
       var warehouse = await warehouseRepository.
           GetByIdAsync(w => w.Id == command.WarehouseId, cancellationToken);

       if (warehouse is null)
       {
           throw new NotFoundException($"Warehouse '{command.WarehouseId}' not found.");
       }
       
       warehouse.Mode = command.Mode;
       warehouse.UpdatedAt = DateTime.UtcNow;
       
       await warehouseRepository.UpdateAsync(warehouse, cancellationToken);
       
       logger.LogInformation("Updated warehouse `{WarehouseId}` status to `{Mode}`.", 
           command.WarehouseId, command.Mode);
    }
}