using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Application.UseCases.TransferRequests.ApproveTransferRequest;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.TransferRequests.CancelTransferRequest;

public class CancelTransferRequestCommandHandler(
    IRepository<TransferRequest> transferRequestRepository,
    ILogger<ApproveTransferRequestCommandHandler> logger) : ICommandHandler<CancelTransferRequestCommand>
{
    public async Task HandleAsync(CancelTransferRequestCommand command, CancellationToken cancellationToken)
    {
        var transfer = await transferRequestRepository
            .GetByIdAsync(t => t.Id == command.TransferId, cancellationToken);

        if (transfer is null)
        {
            throw new NotFoundException($"Transfer request '{command.TransferId}' not found.");
        }
        
        transfer.Status = TransferStatus.Cancelled;
        await transferRequestRepository.UpdateAsync(transfer, cancellationToken);
        
        logger.LogInformation("Cancelled transfer request `{TransferId}`.", command.TransferId);
    }
}