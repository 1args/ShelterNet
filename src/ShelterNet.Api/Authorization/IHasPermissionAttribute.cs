using ShelterNet.Domain.Enums;

namespace ShelterNet.Api.Authorization;

public interface IHasPermissionAttribute
{
    string? Policy { get; }
    
    Permission[] Permissions { get; }
}