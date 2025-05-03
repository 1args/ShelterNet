using System.Security.Claims;

namespace ShelterNet.Application.Abstractions.Auth;

public interface IJwtProvider
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    
    IEnumerable<Claim> ValidateToken(string token);
}