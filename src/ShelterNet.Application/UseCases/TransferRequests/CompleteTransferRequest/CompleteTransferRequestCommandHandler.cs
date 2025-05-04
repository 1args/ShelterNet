using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;
using ApplicationException = ShelterNet.Application.Exceptions.ApplicationException;

namespace ShelterNet.Application.UseCases.TransferRequests.CompleteTransferRequest;

public class CompleteTransferRequestCommandHandler(
    IRepository<TransferRequest> transferRequestRepository,
    IRepository<InventoryItem> inventoryRepository,
    ILogger<CompleteTransferRequestCommandHandler> logger) : ICommandHandler<CompleteTransferRequestCommand>
{
    public async Task HandleAsync(CompleteTransferRequestCommand command, CancellationToken cancellationToken)
    {
        var transfer = await transferRequestRepository.SingleOrDefaultAsync(
            t => t.Id == command.TransferId,
            cancellationToken,
            includes: tr => tr.Resource);
        
        if (transfer is null)
        {
            throw new NotFoundException($"Transfer request '{command.TransferId}' not found.");
        }
        
        var sourceItem = await inventoryRepository.SingleOrDefaultAsync(
            i => i.WarehouseId == transfer.SourceWarehouseId && i.ResourceId == transfer.ResourceId,
            cancellationToken);

        if (sourceItem == null || sourceItem.Quantity < 1)
        {
            throw new ApplicationException("Not enough quantity in source warehouse.");
        }
        
        sourceItem.Quantity -= 1;
        await inventoryRepository.UpdateAsync(sourceItem, cancellationToken);
        
        var destinationItem = await inventoryRepository.SingleOrDefaultAsync(
            i => i.WarehouseId == transfer.DestinationWarehouseId && i.ResourceId == transfer.ResourceId,
            cancellationToken);
        
        if (destinationItem is null)
        {
            destinationItem = new InventoryItem
            {
                WarehouseId = transfer.DestinationWarehouseId,
                ResourceId = transfer.ResourceId,
                Quantity = 1,
                BatchNumber = sourceItem.BatchNumber,
                ExpiryDate = sourceItem.ExpiryDate,
                StorageConditions = sourceItem.StorageConditions
            };
            await inventoryRepository.AddAsync(destinationItem, cancellationToken);
        }
        else
        {
            destinationItem.Quantity += 1;
            await inventoryRepository.UpdateAsync(destinationItem, cancellationToken);
        }
        
        transfer.Status = TransferStatus.Completed;
        transfer.CompletedAt = DateTime.UtcNow;
        
        await transferRequestRepository.UpdateAsync(transfer, cancellationToken);
        
        logger.LogInformation("Completed transfer request '{TransferId}'.", command.TransferId);
    }
}