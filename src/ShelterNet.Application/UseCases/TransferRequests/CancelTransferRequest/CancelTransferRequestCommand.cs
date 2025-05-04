using ShelterNet.Application.Abstractions.Messaging.Commands;

namespace ShelterNet.Application.UseCases.TransferRequests.CancelTransferRequest;

public record CancelTransferRequestCommand(
    Guid TransferId) : ICommand;