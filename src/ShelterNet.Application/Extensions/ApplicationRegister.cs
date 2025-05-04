using Microsoft.Extensions.DependencyInjection;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Application.Abstractions.Messaging.Commands;
using ShelterNet.Application.Abstractions.Messaging.Queries;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Application.Auth;
using ShelterNet.Application.Services;
using ShelterNet.Application.UseCases.Disasters.ProcessDisaster;
using ShelterNet.Application.UseCases.TransferRequests.ApproveTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.CancelTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.CompleteTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.CreateTransferRequest;
using ShelterNet.Application.UseCases.TransferRequests.RejectTransferRequest;
using ShelterNet.Application.UseCases.Users.LoginUser;
using ShelterNet.Application.UseCases.Users.RegisterUser;
using ShelterNet.Application.UseCases.Warehouses.GetAvailableCapacity;
using ShelterNet.Application.UseCases.Warehouses.GetCriticalResources;
using ShelterNet.Application.UseCases.Warehouses.UpdateWarehouseStatus;
using ShelterNet.Contracts.ApiContracts.Responses;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Extensions;

public static class ApplicationRegister 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IWarehouseAlertService, WarehouseAlertService>();
        services.AddScoped<IGeoService, GeoService>();

        services
            .AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>()
            .AddScoped<ICommandHandler<LoginUserCommand, LoginResponse>, LoginUserCommandHandler>()
            .AddScoped<ICommandHandler<ProcessDisasterAlertCommand>, ProcessDisasterAlertCommandHandler>()
            .AddScoped<ICommandHandler<UpdateWarehouseStatusCommand>, UpdateWarehouseStatusCommandHandler>()
            .AddScoped<ICommandHandler<ApproveTransferRequestCommand>, ApproveTransferRequestCommandHandler>()
            .AddScoped<ICommandHandler<CancelTransferRequestCommand>, CancelTransferRequestCommandHandler>()
            .AddScoped<ICommandHandler<CompleteTransferRequestCommand>, CompleteTransferRequestCommandHandler>()
            .AddScoped<ICommandHandler<CreateTransferRequestCommand>, CreateTransferRequestCommandHandler>()
            .AddScoped<ICommandHandler<RejectTransferRequestCommand>, RejectTransferRequestCommandHandler>();

        services
            .AddScoped<IQueryHandler<GetCriticalResourcesQuery, IEnumerable<InventoryItem>>, GetCriticalResourcesQueryHandler>()
            .AddScoped<IQueryHandler<GetAvailableCapacityQuery, AvailableCapacityResponse>, GetAvailableCapacityQueryHandler>();
        
        return services;
    }
}