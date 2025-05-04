using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.Services;

public class WarehouseAlertService(IRepository<User> useRepository,
    IEmailService emailService,
    ILogger<WarehouseAlertService> logger) : IWarehouseAlertService
{
    public async Task NotifyCentralManagementAboutEmergencyAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        var centralManagementUsers = await useRepository
            .ToListAsync(cancellationToken, u => u.Role == UserRole.CentralManagement);

        if (!centralManagementUsers.Any())
        {
            logger.LogWarning("No CentralManagement users found to notify about warehouse emergency.");
            return;
        }
        
        var message = $"🚨 EMERGENCY ALERT: Warehouse {warehouse.Name} (ID: {warehouse.Id}) " +
                      $"at {warehouse.Address} has entered emergency mode. " +
                      $"Coordinates: {warehouse.Latitude}, {warehouse.Longitude}";

        foreach (var user in centralManagementUsers)
        {
            await emailService.SendEmailAsync(user.Email, "Warehouse Emergency Alert", message, cancellationToken);
            logger.LogInformation("Sent emergency alert to `{Email}`.", user.Email);
        }
    }
}