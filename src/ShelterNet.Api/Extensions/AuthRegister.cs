using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShelterNet.Infrastructure.Options;

namespace ShelterNet.Api.Extensions;

public static class AuthRegister
{
    public static IServiceCollection AddAuthenticationRules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
                         ?? throw new InvalidOperationException("JWTOptions is missing.");

        var validationOptions = jwtOptions.TokenValidationOptions;

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = validationOptions.ValidateIssuer,
                    ValidateAudience = validationOptions.ValidateAudience,
                    ValidateIssuerSigningKey = validationOptions.ValidateIssuerSigningKey,
                    ClockSkew = validationOptions.ClockSkew,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                };
            });
        
        return services;
    }
}