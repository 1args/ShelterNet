using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Infrastructure.Auth;
using ShelterNet.Infrastructure.Options;
using AuthorizationOptions = ShelterNet.Infrastructure.Options.AuthorizationOptions;

namespace ShelterNet.Infrastructure.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthRegister(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
        
        services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IJwtProvider, JwtProvider>()
            .AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        
        return services;
    }
}