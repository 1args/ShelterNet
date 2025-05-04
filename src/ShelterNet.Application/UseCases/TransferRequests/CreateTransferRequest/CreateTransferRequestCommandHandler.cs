using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Application.Auth;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.TransferRequests.CreateTransferRequest;

public class CreateTransferRequestCommandHandler(
    IRepository<TransferRequest> transferRequestRepository,
    IAuthService authService,
    ILogger<CreateTransferRequestCommandHandler> logger) : ICommandHandler<CreateTransferRequestCommand>
{
    public async Task HandleAsync(CreateTransferRequestCommand command, CancellationToken cancellationToken)
    {
        var transferRequest = new TransferRequest
        {
            SourceWarehouseId = command.SourceWarehouseId,
            DestinationWarehouseId = command.DestinationWarehouseId,
            ResourceId = command.ResourceId,
            CreatorId = authService.GetUserIdFromAccessToken(),
            Status = TransferStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };

        await transferRequestRepository.AddAsync(transferRequest, cancellationToken);
        logger.LogInformation("Created transfer request `{TransferId}`", transferRequest.Id);
    }
}