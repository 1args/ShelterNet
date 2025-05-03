using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Infrastructure.Auth;
using ShelterNet.Infrastructure.Options;

namespace ShelterNet.Infrastructure.Extensions;

public static class AuthRegister
{
    public static IServiceCollection AddAuthRegister(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        
        services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IJwtProvider, JwtProvider>();
        
        return services;
    }
}