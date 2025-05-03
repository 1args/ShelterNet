namespace ShelterNet.Infrastructure.Options;

public class JwtOptions
{
    public required string Secret { get; set; }
    
    public int AccessTokenExpirationInHours { get; set; } = 24;

    public TokenValidationOptions TokenValidationOptions { get; set; } = new();
}