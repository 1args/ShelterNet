using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShelterNet.Application.Abstractions.Services;
using ShelterNet.Infrastructure.Communication.Email;
using ShelterNet.Infrastructure.Options;

namespace ShelterNet.Infrastructure.Extensions;

public static class SmtpRegister
{
    public static IServiceCollection AddSmtpConfiguration(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var smtpOptions = configuration.GetSection(nameof(SmtpOptions)).Get<SmtpOptions>();
    
        services
            .AddFluentEmail(smtpOptions!.SenderEmail, smtpOptions.SenderName)
            .AddSmtpSender(smtpOptions.Host, smtpOptions.Ports);

        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}