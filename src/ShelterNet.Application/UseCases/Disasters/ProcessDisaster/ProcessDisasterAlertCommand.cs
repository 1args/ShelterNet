using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.UseCases.Disasters.ProcessDisaster;

public sealed record ProcessDisasterAlertCommand(
    DisasterType DisasterType,
    decimal Latitude,
    decimal Longitude,
    decimal RadiusInKm,
    int Severity,
    string Description) : ICommand;