namespace ShelterNet.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging();

        services
            .AddOpenApi()
            .AddHttpContextAccessor()
            .AddControllers();

        services.AddAuthorizationPermissionRequirements();
        services.AddExceptionHandler();
        
        
        return services;
    }
}