using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.TransferRequests.RejectTransferRequest;

public class RejectTransferRequestCommandHandler(
    IRepository<TransferRequest> transferRequestRepository,
    ILogger<RejectTransferRequestCommandHandler> logger) : ICommandHandler<RejectTransferRequestCommand>
{
    public async Task HandleAsync(RejectTransferRequestCommand command, CancellationToken cancellationToken)
    {
        var transfer = await transferRequestRepository
            .GetByIdAsync(t => t.Id == command.TransferId, cancellationToken);
        
        if (transfer is null)
        {
            throw new NotFoundException($"Transfer request '{command.TransferId}' not found.");
        }
        
        transfer.Status = TransferStatus.Rejected;
        await transferRequestRepository.UpdateAsync(transfer, cancellationToken);
        
        logger.LogInformation("Rejected transfer request `{TransferId}`.", command.TransferId);
    }
}