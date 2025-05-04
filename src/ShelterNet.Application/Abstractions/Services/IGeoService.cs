using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Abstractions.Services;

public interface IGeoService
{
    bool IsWarehouseInDangerZone(Warehouse warehouse, Disaster disaster);

    public double CalculateDistance(decimal warehouseLatitude, decimal warehouseLongitude, decimal disasterLatitude, decimal disasterLongitude);

    Warehouse? FindNearestSafeWarehouse(Warehouse endangeredWarehouse, IEnumerable<Warehouse?> allWarehouses);
}