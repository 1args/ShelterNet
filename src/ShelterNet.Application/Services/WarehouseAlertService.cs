using Microsoft.Extensions.Logging;
using ShelterNet.Application.Abstractions.Data;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Services;

public class WarehouseAlertService(
    IRepository<User> userRepository,
    IRepository<Role> roleRepository,
    IEmailService emailService,
    ILogger<WarehouseAlertService> logger) : IWarehouseAlertService
{
    public async Task NotifyCentralManagementAboutEmergencyAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        var role = await roleRepository
                       .SingleOrDefaultAsync(
                           r => r.Id == (int)Domain.Enums.Role.CentralManagement, 
                           cancellationToken) 
                   ?? throw new InvalidOperationException($"Role with id {(int)Domain.Enums.Role.Analyst} does not exist.");
        
        var centralManagementUsers = await userRepository
            .ToListAsync(cancellationToken, u => u.Roles.Contains(role));

        if (!centralManagementUsers.Any())
        {
            logger.LogWarning("No CentralManagement users found to notify about warehouse emergency.");
            return;
        }
        
        var message = $"🚨 EMERGENCY ALERT: Warehouse {warehouse.Name} (ID: {warehouse.Id}) " +
                      $"at {warehouse.Address} has entered emergency mode. " +
                      $"Coordinates: {warehouse.Latitude}, {warehouse.Longitude}. " +
                      $"Follow the protocol!";

        foreach (var user in centralManagementUsers)
        {
            await emailService.SendEmailAsync(user.Email, "Warehouse Emergency Alert", message, cancellationToken);
            logger.LogInformation("Sent emergency alert to `{Email}`.", user.Email);
        }
    }
}