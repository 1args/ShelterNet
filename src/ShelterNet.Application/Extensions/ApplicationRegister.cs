using Microsoft.Extensions.DependencyInjection;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Auth;
using ShelterNet.Application.UseCases.Users.LoginUser;
using ShelterNet.Application.UseCases.Users.RegisterUser;
using ShelterNet.Contracts.ApiContracts.Responses;

namespace ShelterNet.Application.Extensions;

public static class ApplicationRegister 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        services
            .AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>()
            .AddScoped<ICommandHandler<LoginUserCommand, LoginResponse>, LoginUserCommandHandler>();
        
        return services;
    }
}