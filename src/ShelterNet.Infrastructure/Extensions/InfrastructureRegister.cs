using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShelterNet.Infrastructure.Extensions;

public static class InfrastructureRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration);
        services.AddAuthRegister(configuration);
        
        return services;
    }
}