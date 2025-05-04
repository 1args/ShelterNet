using ShelterNet.Application.Abstractions.Messaging.Commands;

namespace ShelterNet.Application.UseCases.TransferRequests.RejectTransferRequest;

public record RejectTransferRequestCommand(
    Guid TransferId) : ICommand;