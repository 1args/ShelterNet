using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.UseCases.Users.RegisterUser;

public sealed class RegisterUserCommandHandler(
    IRepository<User> userRepository,
    IPasswordHasher passwordHasher,
    ILogger<RegisterUserCommandHandler> logger) : ICommandHandler<RegisterUserCommand>
{
    public async Task HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating user with email `{Email}`.", command.Email);
        
        var userExists = await userRepository.AnyAsync(u => u.Email == command.Email, cancellationToken);
        
        if (userExists)
        {
            throw new ConflictException($"User with email {command.Email} already exists.");
        }
        
        var passwordHash = passwordHasher.HashPassword(command.Password);

        var user = new User
        {
            FullName = command.FullName,
            Email = command.Email,
            PasswordHash = passwordHash,
        };
        
        await userRepository.AddAsync(user, cancellationToken);
        logger.LogInformation("User created successfully with ID `{UserId}`.", user.Id);
    }
}