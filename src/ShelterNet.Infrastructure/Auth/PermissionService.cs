using Microsoft.EntityFrameworkCore;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Domain.Enums;
using ShelterNet.Infrastructure.Data.Context;

namespace ShelterNet.Infrastructure.Auth;

public class PermissionService(ApplicationDbContext dbContext) : IPermissionService
{
    public async Task<HashSet<Permission>> GetUserPermissionsAsync(Guid userId)
    {
        var roles = await dbContext.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();
        
        var permissions = roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
        
        return permissions;
    }
}