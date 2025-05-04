using Microsoft.AspNetCore.Mvc;
using ShelterNet.Api.Authorization;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.UseCases.TransferRequests.ApproveTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.CancelTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.CompleteTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.CreateTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.RejectTransferRequest;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Api.Controllers;

[ApiController]
[Route("/api/transfer-requests")]
public class TransferRequestsController(
    ICommandHandler<ApproveTransferRequestCommand> approveTransferRequestHandler,
    ICommandHandler<CancelTransferRequestCommand> cancelTransferRequestHandler,
    ICommandHandler<CompleteTransferRequestCommand> completeTransferRequestHandler,
    ICommandHandler<CreateTransferRequestCommand> createTransferRequestHandler,
    ICommandHandler<RejectTransferRequestCommand> rejectTransferRequestHandler) : ControllerBase
{
    [HttpPatch("approve")]
    [HasPermission(Permission.Access)]
    public async Task<IActionResult> ApproveTransferRequestAsync(
        [FromRoute] ApproveTransferRequestCommand command,
        CancellationToken cancellationToken)
    {
        await approveTransferRequestHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpPatch("cancel")]
    [HasPermission(Permission.Update)]
    public async Task<IActionResult> CancelTransferRequestAsync(
        [FromRoute] CancelTransferRequestCommand command,
        CancellationToken cancellationToken)
    {
        await cancelTransferRequestHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpPatch("complete")]
    [HasPermission(Permission.Update)]
    public async Task<IActionResult> CompleteTransferRequestAsync(
        [FromRoute] CompleteTransferRequestCommand command,
        CancellationToken cancellationToken)
    {
        await completeTransferRequestHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("create")]
    [HasPermission(Permission.Create)]
    public async Task<IActionResult> CreateTransferRequestAsync(
        [FromBody] CreateTransferRequestCommand command,
        CancellationToken cancellationToken)
    {
        await createTransferRequestHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
    
    [HttpPatch("reject")]
    [HasPermission(Permission.Access)]
    public async Task<IActionResult> RejectTransferRequestAsync(
        [FromRoute] RejectTransferRequestCommand command,
        CancellationToken cancellationToken)
    {
        await rejectTransferRequestHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
}