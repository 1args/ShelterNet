using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Domain.Entities;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.Services;

public class GeoService : IGeoService
{
    public bool IsWarehouseInDangerZone(Warehouse warehouse, Disaster disaster)
    {
        var distance = CalculateDistance(
            warehouse.Latitude, warehouse.Longitude,
            disaster.Latitude, disaster.Longitude);

        return distance <= (double)disaster.RadiusInKm;
    }

    public double CalculateDistance(
        decimal warehouseLatitude,
        decimal warehouseLongitude,
        decimal disasterLatitude,
        decimal disasterLongitude)
    {
        var earthRadiusInKm = 6371; 
        
        var dLatitude = ToRadians((double)(disasterLatitude - warehouseLatitude));
        var dLongitude = ToRadians((double)(disasterLongitude - warehouseLongitude));

        var a = Math.Sin(dLatitude / 2) * Math.Sin(dLatitude / 2) +
                Math.Cos(ToRadians((double)warehouseLatitude)) * Math.Cos(ToRadians((double)disasterLatitude)) *
                Math.Sin(dLongitude / 2) * Math.Sin(dLongitude / 2);
        
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        
        return earthRadiusInKm * c;
    }

    public Warehouse? FindNearestSafeWarehouse(Warehouse endangeredWarehouse, IEnumerable<Warehouse?> allWarehouses)
    {
        return allWarehouses
            .Where(w => w.Mode == OperationalMode.Normal && w.Id != endangeredWarehouse.Id)
            .OrderBy(w => CalculateDistance(endangeredWarehouse.Latitude, endangeredWarehouse.Longitude,
                w.Latitude, w.Longitude))
            .FirstOrDefault();
    }
    
    private static double ToRadians(double angle) => Math.PI * angle / 180.0;
}