using Microsoft.AspNetCore.Mvc;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.UseCases.Users.LoginUser;
using ShelterNet.Application.UseCases.Users.RegisterUser;
using ShelterNet.Contracts.ApiContracts.Responses;

namespace ShelterNet.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController(
    ICommandHandler<RegisterUserCommand> registerHandler,
    ICommandHandler<LoginUserCommand, LoginResponse> loginHandler) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        await registerHandler.HandleAsync(command, cancellationToken);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(
        [FromBody] LoginUserCommand command, 
        CancellationToken cancellationToken)
    {
        var response = await loginHandler.HandleAsync(command, cancellationToken);
        return Ok(response);
    }
}