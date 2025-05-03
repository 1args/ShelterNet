using System.Security.Claims;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Application.Abstractions.Data;
using Microsoft.AspNetCore.Http;
using ShelterNet.Application.Exceptions;
using ShelterNet.Domain.Entities;

namespace ShelterNet.Application.Auth;

public class AuthService(
    IRepository<User> userRepository,
    IJwtProvider jwtProvider,
    IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private const string AuthorizationHeader = "Authorization";
    private const string BearerPrefix = "Bearer";
    
    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        }; 
        return jwtProvider.GenerateAccessToken(claims);
    }

    public string? GetAccessTokenFromHeader()
    {
        var header = httpContextAccessor.HttpContext?.Request.Headers[AuthorizationHeader];

        if (header.HasValue && !string.IsNullOrWhiteSpace(header.Value))
        {
            var token = header.Value.ToString();
        
            if (token.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return token[BearerPrefix.Length..].Trim();
            }
        }

        return null;
    }

    public Guid GetUserIdFromAccessToken()
    {
        var accessToken = GetAccessTokenFromHeader();

        if (accessToken is null)
        {
            throw new UnauthorizedException("Access token is missing.");
        };
        
        var claims = jwtProvider.ValidateToken(accessToken).ToArray();
        return GetUserIdFromClaims(claims);
    }

    public string GetUserRoleFromAccessToken()
    {
        var accessToken = GetAccessTokenFromHeader();

        if (accessToken is null)
        {
            throw new UnauthorizedException("Access token is missing.");
        };
        
        var claims = jwtProvider.ValidateToken(accessToken).ToArray();
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
    }
    
    private Guid GetUserIdFromClaims(Claim[] claims)
    {
        var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedException("Invalid user identifier in token.");
        }
        return userId;
    }
}