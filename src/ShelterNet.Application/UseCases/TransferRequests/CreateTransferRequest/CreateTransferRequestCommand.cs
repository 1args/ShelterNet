using ShelterNet.Application.Abstractions.Messaging.Commands;

namespace ShelterNet.Application.UseCases.TransferRequests.CreateTransferRequest;

public record CreateTransferRequestCommand(
    Guid SourceWarehouseId,
    Guid DestinationWarehouseId,
    Guid ResourceId,
    int Quantity) : ICommand;