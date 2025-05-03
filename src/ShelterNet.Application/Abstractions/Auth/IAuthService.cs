using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Abstractions.Auth;

public interface IAuthService
{
    string GenerateAccessToken(User user);
    
    string? GetAccessTokenFromHeader();
    
    Guid GetUserIdFromAccessToken();

    string GetUserRoleFromAccessToken();
}