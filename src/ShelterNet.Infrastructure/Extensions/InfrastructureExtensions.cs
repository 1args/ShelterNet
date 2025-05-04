using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShelterNet.Infrastructure.BackgroundServices;

namespace ShelterNet.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration)
            .AddAuthRegister(configuration)
            .AddSmtpConfiguration(configuration)
            .AddAuthRegister(configuration)
            .AddHostedService<WarehouseMonitoringBackgroundService>();
        
        return services;
    }
}