using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Contracts.ApiContracts.Responses;

namespace ShelterNet.Application.UseCases.Users.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password) : ICommand<LoginResponse>;