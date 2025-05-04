using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Abstractions.Services;

public interface IAuthService
{
    string GenerateAccessToken(User user);
    
    string? GetAccessTokenFromHeader();
    
    Guid GetUserIdFromAccessToken();

    string GetUserRoleFromAccessToken();
}