using ShelterNet.Domain.Enums;

namespace ShelterNet.Application.Abstractions.Auth;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetUserPermissionsAsync(Guid userId);
}