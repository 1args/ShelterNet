using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Infrastructure.BackgroundServices;

public class WarehouseMonitoringBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<WarehouseMonitoringBackgroundService> logger) : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);
        
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Warehouse Monitoring Service is starting...");

        using var scope = serviceProvider.CreateScope();
        
        var warehouseRepository = scope.ServiceProvider.GetRequiredService<IRepository<Warehouse>>();
        var alertService = scope.ServiceProvider.GetRequiredService<IWarehouseAlertService>();

        try
        {
            var emergencyWarehouses = await warehouseRepository.
                    GetAllAsync(
                        stoppingToken,
                        w => w.Mode == OperationalMode.Emergency && 
                             (!w.UpdatedAt.HasValue || w.UpdatedAt.Value > DateTime.UtcNow.AddMinutes(-5)));

            foreach (var warehouse in emergencyWarehouses)
            {
                logger.LogInformation("Warehouse `{Id}` is in emergency mode, sending alerts.", warehouse.Id);
                await alertService.NotifyCentralManagementAboutEmergencyAsync(warehouse, stoppingToken);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Warehouse monitoring service failed.");
            throw;
        }
        
        await Task.Delay(_checkInterval, stoppingToken);
        
        logger.LogInformation("Warehouse Monitoring Service is stopping.");
    }
}