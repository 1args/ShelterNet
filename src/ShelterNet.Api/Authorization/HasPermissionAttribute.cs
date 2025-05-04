using Microsoft.AspNetCore.Authorization;
using ShelterNet.Domain.Enums;

namespace ShelterNet.Api.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute, IHasPermissionAttribute
{
    public Permission[] Permissions { get; set; }

    public HasPermissionAttribute(params Permission[] permissions)
    {
        Permissions = permissions;
        Policy = string.Join(' ', permissions.Order().Select(p => p.ToString()));
    }
}