using Microsoft.AspNetCore.Mvc;
using ShelterNet.Api.Authorization;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.UseCases.Disasters.ProcessDisaster;
using ShelterNet.Domain.Enums;
using ShelterNet.Infrastructure.Auth;

namespace ShelterNet.Api.Controllers;

[ApiController]
[Route("/api/disasters")]
public class DisastersController(
    ICommandHandler<ProcessDisasterAlertCommand> disasterAlertHandler) : ControllerBase
{
    [HttpPost("alert")]
    [HasPermission(Permission.Create)]
    public async Task<IActionResult> DisasterAlertAsync(
        [FromBody] ProcessDisasterAlertCommand command,
        CancellationToken cancellationToken)
    {
        await disasterAlertHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
}