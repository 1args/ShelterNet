using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.Warehouses.UpdateWarehouseStatus;

public sealed record UpdateWarehouseStatusCommand(
    Guid WarehouseId,
    OperationalMode Mode) : ICommand;