using ShelterNet.Application.Abstractions.Messaging.Commands;

namespace ShelterNet.Application.UseCases.TransferRequests.CompleteTransferRequest;

public record CompleteTransferRequestCommand(
    Guid TransferId) : ICommand;