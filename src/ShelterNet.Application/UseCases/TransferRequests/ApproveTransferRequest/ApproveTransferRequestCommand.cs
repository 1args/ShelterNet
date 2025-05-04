using ShelterNet.Application.Abstractions.Messaging.Commands;

namespace ShelterNet.Application.UseCases.TransferRequests.ApproveTransferRequest;

public record ApproveTransferRequestCommand(
    Guid TransferId) : ICommand;