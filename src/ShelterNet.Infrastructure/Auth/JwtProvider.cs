using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Application.Exceptions;
using ShelterNet.Infrastructure.Options;

namespace ShelterNet.Infrastructure.Auth;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;
    
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var securityTokenDescriptor = GetSecurityTokenDescriptor(
            _options.Secret,
            _options.AccessTokenExpirationInHours,
            claims.ToArray());

        var accessToken = tokenHandler.CreateToken(securityTokenDescriptor);
        
        return tokenHandler.WriteToken(accessToken);
    }
    
    public IEnumerable<Claim> ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token cannot be empty.");
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationOptions = _options.TokenValidationOptions;

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = tokenValidationOptions.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
            ValidateIssuer = tokenValidationOptions.ValidateIssuer,
            ValidateAudience = tokenValidationOptions.ValidateAudience,
            ValidateLifetime = true,
            ClockSkew = tokenValidationOptions.ClockSkew
        };

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return claimsPrincipal.Claims;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new TokenExpiredException("Token has expired.");
        }
        catch (SecurityTokenException)
        {
            throw new InvalidTokenException("Invalid token");
        }
    }

    private static SecurityTokenDescriptor GetSecurityTokenDescriptor(
        string secretKey,
        int expirationHours, 
        params Claim[]? claims)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            SecurityAlgorithms.HmacSha256Signature);
        
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expirationHours),
            SigningCredentials = signingCredentials
        };
    }
}