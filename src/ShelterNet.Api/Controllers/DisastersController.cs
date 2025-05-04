using Microsoft.AspNetCore.Mvc;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.UseCases.Disasters.ProcessDisaster;

namespace ShelterNet.Api.Controllers;

[ApiController]
[Route("/api/disasters")]
public class DisastersController(
    ICommandHandler<ProcessDisasterAlertCommand> disasterAlertHandler) : ControllerBase
{
    [HttpPost("alert")]
    public async Task<IActionResult> DisasterAlertAsync(
        [FromBody] ProcessDisasterAlertCommand command,
        CancellationToken cancellationToken)
    {
        await disasterAlertHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }
}