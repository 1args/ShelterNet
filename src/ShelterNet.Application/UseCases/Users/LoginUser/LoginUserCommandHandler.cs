using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Users.LoginUser;

public sealed class LoginUserCommandHandler(
    IRepository<User> userRepository,
    IPasswordHasher passwordHasher,
    IAuthService authService,
    ILogger<LoginUserCommandHandler> logger) : ICommandHandler<LoginUserCommand, LoginResponse>
{
    public async Task<LoginResponse> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Login attempt for `{Email}`...", command.Email);
        
        var user = await userRepository
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken, tracking: false);
        
        var verifiedPasswordHash  = passwordHasher.VerifyHashedPassword(command.Password, user!.PasswordHash);
        
        if (user is null || !verifiedPasswordHash)
        {
            logger.LogWarning("Login failed for `{Email}`. Invalid credentials.", command.Email);
            throw new UnauthorizedException("Invalid email or password.");
        }
        
        var accessToken = authService.GenerateAccessToken(user);
        
        return new LoginResponse(accessToken);
    }
}