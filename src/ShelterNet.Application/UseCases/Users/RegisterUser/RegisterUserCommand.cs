using ShelterNet.Application.Abstractions.Messaging.Commands;

namespace ShelterNet.Application.UseCases.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string FullName,
    string Email,
    string Password) : ICommand;