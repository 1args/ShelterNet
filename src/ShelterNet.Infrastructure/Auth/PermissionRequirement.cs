using Microsoft.AspNetCore.Authorization;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Infrastructure.Auth;

public class PermissionRequirement(params Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}