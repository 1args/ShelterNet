using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.Disasters.ProcessDisaster;

public sealed class ProcessDisasterAlertCommandHandler(
    IRepository<Disaster> disasterRepository,
    IRepository<Warehouse> warehouseRepository,
    IGeoService geoService,
    ILogger<ProcessDisasterAlertCommandHandler> logger) : ICommandHandler<ProcessDisasterAlertCommand>
{
    public async Task HandleAsync(ProcessDisasterAlertCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Processing disaster alert: {DisasterType} (Severity: {Severity})",
            command.DisasterType,
            command.Severity);

        var disaster = new Disaster
        {
            Type = command.DisasterType,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            RadiusInKm = command.RadiusInKm,
            Severity = command.Severity,
            StartTime = DateTime.UtcNow,
            Description = command.Description,
        };
        
        await disasterRepository.AddAsync(disaster, cancellationToken);
        
        var allWarehouses = await warehouseRepository.GetAllAsync(cancellationToken);

        var endangeredWarehouses = allWarehouses
            .Where(w => geoService.IsWarehouseInDangerZone(w, disaster))
            .ToList();

        foreach (var warehouse in endangeredWarehouses)
        {
            warehouse.Mode = OperationalMode.Emergency;
            await warehouseRepository.UpdateAsync(warehouse, cancellationToken);
        }
        
        logger.LogInformation(
            "Processed disaster alert. {Count} warehouses affected.",
            endangeredWarehouses.Count);
    }
}