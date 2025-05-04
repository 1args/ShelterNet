using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.TransferRequests.ApproveTransferRequest;

public class ApproveTransferRequestCommandHandler(
    IRepository<TransferRequest> transferRequestRepository,
    ILogger<ApproveTransferRequestCommandHandler> logger) : ICommandHandler<ApproveTransferRequestCommand>
{
    public async Task HandleAsync(ApproveTransferRequestCommand command, CancellationToken cancellationToken)
    {
        var transfer = await transferRequestRepository
            .GetByIdAsync(t => t.Id == command.TransferId, cancellationToken);

        if (transfer is null)
        {
            throw new NotFoundException($"Transfer request '{command.TransferId}' not found.");
        }
        
        transfer.Status = TransferStatus.Approved;
        await transferRequestRepository.UpdateAsync(transfer, cancellationToken);
        
        logger.LogInformation("Approved transfer request `{TransferId}`.", command.TransferId);
    }
}