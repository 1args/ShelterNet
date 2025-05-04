using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Abstractions.Services;

public interface IWarehouseAlertService
{
    Task NotifyCentralManagementAboutEmergencyAsync(Warehouse warehouse, CancellationToken cancellationToken);
}